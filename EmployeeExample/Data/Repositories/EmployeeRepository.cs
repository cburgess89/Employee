using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllSorted();
    }

    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly SolutionDatabaseContext _db;
        private readonly IMapper _mapper;

        public EmployeeRepository(SolutionDatabaseContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employee>> GetAllSorted()
        {
            IEnumerable<Employee> _all = await _db.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).AsNoTracking().ToListAsync();
            return _all;
        }

        //Base repository abstracts the standard functions
    }
}
