using Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mediatr.Queries
{
    public  class GetEmployeeByIdQuery: IRequest<Employee_DTO>
    {
        public int Id { get; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;    
        }

    }
}
