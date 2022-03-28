using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class Employee_DTO
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Department { get; set; }
        public DateTime? HireDate { get; set; }
    }

    public class Employee_DTO_InsertUpdate
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Department { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
