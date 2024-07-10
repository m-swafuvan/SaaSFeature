using SaaSFeature.Domain.Entities;
using System.Collections.Generic;

namespace SaaSFeature.Infrastructure.Configurations
{
    public class TenantConfiguration
    {
        public List<Tenant> Tenants { get; set; }
        public string MissingTenantUrl { get; set; }
    }
}
