using Entities.DTO;
using MediatR;

namespace Data.Mediatr.Queries
{    
    public class GetAllEmployeesQuery : IRequest<List<Employee_DTO_Mini>>
    {
        
    }
}
