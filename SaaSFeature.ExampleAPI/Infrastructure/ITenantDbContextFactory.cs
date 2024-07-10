namespace SaaSFeature.ExampleAPI.Infrastructure
{
    public interface ITenantDbContextFactory
    {
        StudentDbContext CreateDbContext(string connectionString);
    }
}
