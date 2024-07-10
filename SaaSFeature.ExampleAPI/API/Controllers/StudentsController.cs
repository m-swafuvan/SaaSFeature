using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaSFeature.Application.Interfaces;
using SaaSFeature.ExampleAPI.Domain.Entities;
using SaaSFeature.ExampleAPI.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSFeature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly ITenantDbContextFactory _dbContextFactory;

        public StudentsController(ITenantService tenantService, ITenantDbContextFactory dbContextFactory)
        {
            _tenantService = tenantService;
            _dbContextFactory = dbContextFactory;
        }

        private StudentDbContext GetDbContext()
        {
            var tenant = _tenantService.GetTenantByHost(Request.Host.Value);
            if (tenant == null) throw new KeyNotFoundException("Tenant not found");
            return _dbContextFactory.CreateDbContext(tenant.ConnectionString);
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            using var context = GetDbContext();
            return await context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            using var context = GetDbContext();
            var student = await context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            using var context = GetDbContext();
            context.Students.Add(student);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            using var context = GetDbContext();
            context.Entry(student).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Students.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            using var context = GetDbContext();
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
