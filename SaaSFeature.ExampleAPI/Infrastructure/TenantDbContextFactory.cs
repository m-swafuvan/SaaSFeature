using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace SaaSFeature.ExampleAPI.Infrastructure
{
    public class TenantDbContextFactory : ITenantDbContextFactory, IDesignTimeDbContextFactory<StudentDbContext>
    {
        public StudentDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new StudentDbContext(optionsBuilder.Options);
        }

        public StudentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentDbContext>();
            optionsBuilder.UseSqlServer("YourDefaultConnectionString");

            return new StudentDbContext(optionsBuilder.Options);
        }
    }
}
