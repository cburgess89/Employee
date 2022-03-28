using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Data
{
    public interface IDbInitializer
    {
        // Adds some default values to the Db      
        void SeedData();
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }
        
        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SolutionDatabaseContext>())
                {
                    //confirm there are no users, then seed supplied employees
                    if (!context.Employees.Any())
                    {
                        context.Employees.AddRangeAsync(
                            new Employee
                            {
                                Id = 1,
                                LastName = "Jackson",
                                FirstName = "Alberta",
                                Department = "Finance",
                                HireDate = DateTime.Parse("6/5/2007")

                            }
                            , new Employee
                            {
                                Id = 2,
                                LastName = "Bennett",
                                FirstName = "Alicia",
                                Department = "Human Resources",
                                HireDate = DateTime.Parse("4/15/2001")
                            }
                            , new Employee
                            {
                                Id = 3,
                                LastName = "Avent",
                                FirstName = "Donna",
                                Department = "Revenue",
                                HireDate = DateTime.Parse("4/20/2009")
                            }
                            , new Employee
                            {
                                Id = 4,
                                LastName = "Holder",
                                FirstName = "Duane",
                                Department = "Human Services",
                                HireDate = DateTime.Parse("8/15/2020")
                            }
                        );
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
