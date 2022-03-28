using AutoMapper;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTO;

namespace EmployeeExample.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("API/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public EmployeesController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<ActionResult<List<Employee_DTO_Mini>>> GetEmployees()
        {
            try
            {
                var _returned = await _repositoryWrapper.EmployeeRepository.GetAllSorted();
                return Ok(_mapper.Map<Employee_DTO_Mini[]>(_returned));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + ". Inner Exception: " + e.InnerException.Message);
            }
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee_DTO>> GetEmployee(int id)
        {
            try
            {
                var _returned = await _repositoryWrapper.EmployeeRepository.FindSingleByCondition(e => e.Id == id);
                if(_returned == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<Employee_DTO>(_returned));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + ". Inner Exception: " + e.InnerException.Message);
            }
        }

        [HttpPost(Name = "PostEmployee")]        
        public async Task<ActionResult<Employee_DTO>> PostEmployee(Employee_DTO_InsertUpdate _newEmployee)
        {
            try
            {
                if (_newEmployee == null)
                {
                    return BadRequest($"No payload sent");
                }

                Employee _employee = _mapper.Map<Employee>(_newEmployee);
                _repositoryWrapper.EmployeeRepository.Create(_employee);
                await _repositoryWrapper.SaveAsync();                
                return CreatedAtRoute("GetEmployee", new { id = _employee.Id }, _employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + ". Inner Exception: " + e.InnerException.Message);
            }
        }
    }
}
