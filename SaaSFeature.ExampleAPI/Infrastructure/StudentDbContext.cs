using Microsoft.EntityFrameworkCore;
using SaaSFeature.ExampleAPI.Domain.Entities;

namespace SaaSFeature.ExampleAPI.Infrastructure
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
