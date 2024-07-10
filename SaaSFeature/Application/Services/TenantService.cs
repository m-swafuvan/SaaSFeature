using SaaSFeature.Application.Interfaces;
using SaaSFeature.Domain.Entities;
using SaaSFeature.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaaSFeature.Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly List<Tenant> _tenants;

        public TenantService(TenantConfiguration tenantConfiguration)
        {
            _tenants = tenantConfiguration.Tenants ?? throw new ArgumentNullException(nameof(tenantConfiguration.Tenants));
        }

        public Tenant GetTenantByHost(string host)
        {
            return _tenants.FirstOrDefault(t => t.Host.Equals(host, StringComparison.OrdinalIgnoreCase));
        }

        public Tenant GetTenantById(int id)
        {
            return _tenants.FirstOrDefault(t => t.Id == id);
        }
    }
}