using AutoMapper;
using Data.Mediatr.Commands;
using Data.Mediatr.Queries;
using Data.Repositories;
using Entities.DTO;
using Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mediatr.Handlers
{
    public class CreateEmployeeHandler: IRequestHandler<Employee_DTO_InsertUpdate, Employee_DTO>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Employee_DTO> Handle(Employee_DTO_InsertUpdate request, CancellationToken cancellationToken)
        {
            Employee _employee = _mapper.Map<Employee>(request);
            _repositoryWrapper.EmployeeRepository.Create(_employee);
            await _repositoryWrapper.SaveAsync();
            return _mapper.Map<Employee_DTO>(_employee);
        }
    }
}
