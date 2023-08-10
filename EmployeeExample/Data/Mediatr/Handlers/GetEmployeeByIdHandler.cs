using AutoMapper;
using Data.Mediatr.Queries;
using Data.Repositories;
using Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mediatr.Handlers
{
    public class GetEmployeeByIdHandler: IRequestHandler<GetEmployeeByIdQuery, Employee_DTO>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetEmployeeByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Employee_DTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var _returned = await _repositoryWrapper.EmployeeRepository.FindSingleByCondition(e => e.Id == request.Id);
            if (_returned == null)
            {
                return null;
            }
            return _mapper.Map<Employee_DTO>(_returned);
        }        
    }
}
