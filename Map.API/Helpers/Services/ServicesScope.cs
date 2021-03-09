using System.IO;
using Map.API.Helpers.Middleware;
using Map.API.Managers.Implementation;
using Map.API.Managers.Interfaces;
using Map.Core.Interfaces;
using Map.Core.Interfaces.IShapeRepositories;
using Map.Infrastructure.Data;
using Map.Infrastructure.Persistence;
using Map.Infrastructure.Persistence.ShapeRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Map.API.Helpers.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServicesScope
    {
        private const string CORS_POLICY = "CorsPolicy";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitControllers(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MapApplication", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Map.API.xml");
                c.IncludeXmlComments(filePath);
                var corePath = Path.Combine(System.AppContext.BaseDirectory, "Map.Core.xml");
                c.IncludeXmlComments(corePath);
            });
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitCors(this IServiceCollection services)
        {
            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_POLICY,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection InitContext(this IServiceCollection services, string connectionString)
        {
            // Context
            services.AddDbContext<MapDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitDependenciesInjection(this IServiceCollection services)
        {
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped(typeof(IRepo<>), typeof(Repo<>))
                .AddScoped<IShapeRepo, ShapeRepositories>()
                .AddScoped<IShapeManager, ShapeManager>()
                .AddHttpContextAccessor();
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="isDevelopment"></param>
        /// <returns></returns>
        public static IApplicationBuilder InitMiddleware(this IApplicationBuilder app, bool isDevelopment = false)
        {
            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MapApplication v1"));
            }
            app.UseCors(CORS_POLICY);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseMiddleware<SharedRequestMiddleware>();
            return app;
        }
    }
}
