using Microsoft.EntityFrameworkCore;

namespace StudentsGrpcService.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Models.StudentModel>? Students { get; set; }
    }
}
