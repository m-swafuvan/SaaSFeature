using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaaSFeature.Application.Interfaces;
using SaaSFeature.Application.Services;
using SaaSFeature.Infrastructure.Configurations;
using SaaSFeature.Infrastructure.Middlewares;

namespace SaaSFeature.Infrastructure.Extensions
{
    public static class SaaSMiddlewareExtension
    {
        public static IServiceCollection AddSaaSFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var tenantConfiguration = new TenantConfiguration();
            configuration.GetSection("TenantConfiguration").Bind(tenantConfiguration);
            services.AddSingleton(tenantConfiguration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITenantService, TenantService>();
            //services.AddScoped<TenantResolverMiddleware>();
            return services;
        }

        public static IApplicationBuilder UseSaaSFeature(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantResolverMiddleware>();
            return app;
        }
    }
}