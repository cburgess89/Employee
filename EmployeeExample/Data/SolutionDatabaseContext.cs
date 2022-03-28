using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Data
{
    public class SolutionDatabaseContext: DbContext
    {
        public SolutionDatabaseContext()
        {
        }

        public SolutionDatabaseContext(DbContextOptions<SolutionDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.LastName)
                    //.IsRequired()
                    .HasMaxLength(100)                    
                    ;

                entity.Property(e => e.FirstName)
                    //.IsRequired()
                    .HasMaxLength(100)
                    ;

                entity.Property(e => e.Department)
                    //.IsRequired()
                    .HasMaxLength(100)
                    ;               

                entity.Property(e => e.HireDate)
                    ;                    
            });            
        }
    }
}