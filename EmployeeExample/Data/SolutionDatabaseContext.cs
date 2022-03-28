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
                    //.HasDefaultValueSql("(N'')")
                    ;               

                entity.Property(e => e.HireDate)
                    //.IsRequired()
                    //.HasDefaultValueSql("(N'')")
                    ;                    
            });

            //modelBuilder.Entity<Employee>().HasData(
            //    new Employee
            //    {
            //        Id = 1,
            //        LastName = "Jackson",
            //        FirstName = "Alberta",
            //        Department = "Finance",
            //        HireDate = DateTime.Parse("6/5/2007")

            //    }
            //    ,new Employee 
            //    {
            //        Id = 2,
            //        LastName = "Bennett",
            //        FirstName = "Alicia",
            //        Department = "Human Resources",
            //        HireDate = DateTime.Parse("4/15/2001")
            //    }
            //    , new Employee
            //    {
            //        Id = 3,
            //        LastName = "Avent",
            //        FirstName = "Donna",
            //        Department = "Revenue",
            //        HireDate = DateTime.Parse("4/20/2009")
            //    }
            //    , new Employee
            //    {
            //        Id = 4,
            //        LastName = "Holder",
            //        FirstName = "Duane",
            //        Department = "Human Services",
            //        HireDate = DateTime.Parse("8/15/2020")
            //    }    
            //);
        }
    }
}