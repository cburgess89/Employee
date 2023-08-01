using Entities.DTO;
using MediatR;
using Data.Mediatr.Queries;
using Data.Repositories;
using AutoMapper;

namespace Data.Mediatr.Handlers
{
    public class GetAllOrdersHandler: IRequestHandler<GetAllEmployeesQuery, List<Employee_DTO_Mini>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        public GetAllOrdersHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        public async Task<List<Employee_DTO_Mini>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var _returned = await _repositoryWrapper.EmployeeRepository.GetAllSorted();
            return _mapper.Map<Employee_DTO_Mini[]>(_returned).ToList();
        }
        
    }

    
}
