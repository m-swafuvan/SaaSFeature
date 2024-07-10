using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SaaSFeature.ExampleAPI.Infrastructure;
using SaaSFeature.Infrastructure.Extensions;

namespace SaaSFeature.ExampleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSaaSFeature(Configuration);

            //services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            //{
            //    var tenantResolver = serviceProvider.GetRequiredService<TenantResolverMiddleware>();
            //    var tenant = tenantResolver.GetCurrentTenant();
            //    if (tenant != null)
            //    {
            //        options.UseSqlServer(tenant.ConnectionString);
            //    }
            //});

            services.AddControllers();

            services.AddSaaSFeature(Configuration);

            services.AddSingleton<ITenantDbContextFactory, TenantDbContextFactory>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SaaS Feature Example API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SaaS Feature Example API");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSaaSFeature();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
