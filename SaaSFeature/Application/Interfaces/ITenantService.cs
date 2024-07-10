using SaaSFeature.Domain.Entities;

namespace SaaSFeature.Application.Interfaces
{
    public interface ITenantService
    {
        Tenant GetTenantByHost(string host);
        Tenant GetTenantById(int id);
    }
}
