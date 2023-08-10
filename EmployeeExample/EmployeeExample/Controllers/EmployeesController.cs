using AutoMapper;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTO;
using MediatR;
using Data.Mediatr.Queries;
using Data.Mediatr.Commands;

namespace EmployeeExample.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("API/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<ActionResult<List<Employee_DTO_Mini>>> GetEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);            
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee_DTO>> GetEmployee(int id)
        {
            var query = new GetEmployeeByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();            
        }

        [HttpPost(Name = "PostEmployee")]        
        public async Task<ActionResult<Employee_DTO>> PostEmployee(Employee_DTO_InsertUpdate command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetEmployee", new { id = result.Id }, result);            
        }
    }
}
