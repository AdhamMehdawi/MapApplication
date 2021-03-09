using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Map.API.Helpers.SharedResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Map.API.Helpers.Middleware
{
    /// <summary>
    /// Middleware to handle all requests and Exceptions
    /// </summary>
    public class SharedRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public SharedRequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("Map Application API");
        }
        /// <summary>
        /// Process the Request and Catch Exceptions
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var eventId = DateTime.UtcNow.Ticks.ToString();
            var actionContext = new ActionContext { HttpContext = context };
            try
            {
                await _next(context);
                sw.Stop();
                var logMessage = GetRequestResponseData(context, sw.ElapsedMilliseconds);
                _logger.LogInformation(new EventId(0, eventId), logMessage);
            }
            catch (ShredValidationException ve)
            {
                var response = new SharedResponseResult<object>(null, System.Net.HttpStatusCode.BadRequest, ve.Notify, ve.Messages);
                await response.ExecuteResultAsync(actionContext);
                sw.Stop();
                var logMessage = GetRequestResponseData(context, sw.ElapsedMilliseconds);
                _logger.LogWarning(new EventId(0, eventId), logMessage);
                await HandleExceptionAsync(context, response).ConfigureAwait(true);
            }
            catch (ShredBadRequestException be)
            {
                var response = new SharedResponseResult<object>(null, System.Net.HttpStatusCode.BadRequest, be.Notify, be.Messages);
                await response.ExecuteResultAsync(actionContext);
                sw.Stop();
                var logMessage = GetRequestResponseData(context, sw.ElapsedMilliseconds);
                _logger.LogWarning(new EventId(0, eventId), logMessage);
                await HandleExceptionAsync(context, response).ConfigureAwait(true);
            }
            catch (ShredNotFoundException be)
            {
                var response = new SharedResponseResult<object>(null, System.Net.HttpStatusCode.NotFound, be.Notify, be.Messages);
                await response.ExecuteResultAsync(actionContext);
                sw.Stop();
                var logMessage = GetRequestResponseData(context, sw.ElapsedMilliseconds);
                _logger.LogWarning(new EventId(0, eventId), logMessage);
                await HandleExceptionAsync(context, response).ConfigureAwait(true);
            }
            catch (SharedAuthorizationException ae)
            {
                var response = new SharedResponseResult<object>(null, System.Net.HttpStatusCode.Forbidden, true, ae.Messages);
                await response.ExecuteResultAsync(actionContext);
                sw.Stop();
                var logMessage = GetRequestResponseData(context, sw.ElapsedMilliseconds);
                _logger.LogError(new EventId(0, eventId), logMessage);
                await HandleExceptionAsync(context, response).ConfigureAwait(true);
            }
            catch (SharedAuthenticationException ae)
            {
                var response = new SharedResponseResult<object>(null, System.Net.HttpStatusCode.Unauthorized, true, ae.Messages);
                await response.ExecuteResultAsync(actionContext);
                sw.Stop();
                var logMessage = GetRequestResponseData(context, sw.ElapsedMilliseconds);
                _logger.LogError(new EventId(0, eventId), logMessage);
                await HandleExceptionAsync(context, response).ConfigureAwait(true);
            }
            catch (Exception e)
            {
                var message = $"Unknown Exception Please Check Log {eventId}";
                var response = new SharedResponseResult<object>(null, System.Net.HttpStatusCode.InternalServerError, true, message);
                await response.ExecuteResultAsync(actionContext);
                sw.Stop();
                var logMessage = GetRequestResponseData(context, sw.ElapsedMilliseconds);
                _logger.LogCritical(new EventId(0, eventId), e, logMessage);
                await HandleExceptionAsync(context, response).ConfigureAwait(true);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, IStatusCodeActionResult ex)
        {
            var result = JsonConvert.SerializeObject(ex);
            var contentType = string.IsNullOrEmpty(context.Request.ContentType)
                ? "application/json"
                : context.Request.ContentType;
            context.Response.ContentType = contentType;
            context.Response.StatusCode = ex.StatusCode ?? 500;
            return context.Response.WriteAsync(result);
        }
        private string GetRequestResponseData(HttpContext context, long elapsedMilliseconds)
        {
            var request = context.Request;
            var response = context.Response;
            var requestLogMessage = $"{DateTime.UtcNow:s}\nREQUEST:\n{request.Method} - {request.Path.Value}{request.QueryString}";
            requestLogMessage += $"\nContentType: {request.ContentType ?? "Not specified"}";
            requestLogMessage += $"\nHost: {request.Host}";
            requestLogMessage += $"\nRESPONSE:\nStatus Code: {response.StatusCode}";
            requestLogMessage += $"\nRequestTime: {elapsedMilliseconds}";
            return requestLogMessage;
        }
    }
}
