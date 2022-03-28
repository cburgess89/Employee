using Entities.Model;
using Entities.DTO;
using AutoMapper;

namespace Entities.AutoMapper
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Employee, Employee_DTO>().ReverseMap();
        }
    }
}
