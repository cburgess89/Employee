using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository EmployeeRepository { get; }
        void Save();
        Task SaveAsync();
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly SolutionDatabaseContext _repositoryContext;
        private readonly IMapper _mapper;

        private IEmployeeRepository _employeeRepository;


        public RepositoryWrapper(SolutionDatabaseContext repositoryContext, IMapper mapper)
        {
            _repositoryContext = repositoryContext;
            _mapper = mapper;
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_repositoryContext, _mapper);
                }

                return _employeeRepository;
            }
        }



        public void Save()
        {
            _repositoryContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
