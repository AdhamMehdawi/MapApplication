using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Map.API.Helpers.SharedResponse
{
    /// <summary>
    /// 
    /// </summary>
    public class ShredValidationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public string[] Messages;
        /// <summary>
        /// 
        /// </summary>
        public bool Notify;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ShredValidationException(params string[] message)
        {
            Messages = message;
            Notify = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify"></param>
        /// <param name="message"></param>
        public ShredValidationException(bool notify = false, params string[] message)
        {
            Messages = message;
            Notify = notify;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="notify"></param>
        public ShredValidationException(ModelStateDictionary modelState, bool notify = false) : this(notify, modelState.Values.SelectMany(mse => mse.Errors.Select(err => err.ErrorMessage)).ToArray())
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShredBadRequestException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public string[] Messages;
        /// <summary>
        /// 
        /// </summary>
        public bool Notify;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ShredBadRequestException(params string[] message)
        {
            Messages = message;
            Notify = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify"></param>
        /// <param name="message"></param>
        public ShredBadRequestException(bool notify = false, params string[] message)
        {
            Messages = message;
            Notify = notify;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="notify"></param>
        public ShredBadRequestException(ModelStateDictionary modelState, bool notify = false) : this(notify, modelState.Values.SelectMany(mse => mse.Errors.Select(err => err.ErrorMessage)).ToArray())
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShredNotFoundException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public string[] Messages;
        /// <summary>
        /// 
        /// </summary>
        public bool Notify;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ShredNotFoundException(params string[] message)
        {
            Messages = message;
            Notify = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify"></param>
        /// <param name="message"></param>
        public ShredNotFoundException(bool notify = false, params string[] message)
        {
            Messages = message;
            Notify = notify;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="notify"></param>
        public ShredNotFoundException(ModelStateDictionary modelState, bool notify = false) : this(notify, modelState.Values.SelectMany(mse => mse.Errors.Select(err => err.ErrorMessage)).ToArray())
        {
        }
    }
}
