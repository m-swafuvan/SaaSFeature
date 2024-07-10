using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SaaSFeature.Application.Interfaces;
using SaaSFeature.Domain.Entities;
using SaaSFeature.Infrastructure.Configurations;
using System;
using System.Threading.Tasks;

namespace SaaSFeature.Infrastructure.Middlewares
{
    public class TenantResolverMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TenantConfiguration _tenantConfig;

        public TenantResolverMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, TenantConfiguration tenantConfig)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _tenantConfig = tenantConfig ?? throw new ArgumentNullException(nameof(tenantConfig));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
                var host = context.Request.Host.Value;
                var tenant = tenantService.GetTenantByHost(host);

                if (tenant == null)
                {
                    context.Response.Redirect(_tenantConfig.MissingTenantUrl);
                    return;
                }

                context.Items["CurrentTenant"] = tenant;
            }

            await _next(context);
        }
    }
}

