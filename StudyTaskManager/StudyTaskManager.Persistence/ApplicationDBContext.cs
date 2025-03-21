using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Persistence
{
    public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
