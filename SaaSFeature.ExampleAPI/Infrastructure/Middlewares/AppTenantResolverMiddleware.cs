using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SaaSFeature.Application.Interfaces;
using System.Threading.Tasks;

namespace SaaSFeature.ExampleAPI.Infrastructure.Middlewares
{
    public class AppTenantResolverMiddleware
    {
        private readonly RequestDelegate _next;

        public AppTenantResolverMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantService tenantService, ITenantDbContextFactory dbContextFactory)
        {
            var host = context.Request.Host.Value;
            var tenant = tenantService.GetTenantByHost(host);
            if (tenant != null)
            {
                var dbContext = dbContextFactory.CreateDbContext(tenant.ConnectionString);
                context.RequestServices.GetRequiredService<StudentDbContextAccessor>().DbContext = dbContext;
            }

            await _next(context);
        }
    }

    public class StudentDbContextAccessor
    {
        public StudentDbContext DbContext { get; set; }
    }
}
