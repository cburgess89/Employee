using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using AutoMapper;

namespace Data.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {

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
        
        //Base repository abstracts the standard functions
    }
}
