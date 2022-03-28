using Entities.Models;
using Entities.DTO;
using AutoMapper;

namespace Entities.AutoMapper
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Employee, Employee_DTO>().ReverseMap();
            CreateMap<Employee, Employee_DTO_InsertUpdate>().ReverseMap();
            CreateMap<Employee, Employee_DTO_Mini>().ReverseMap();
        }
    }
}
