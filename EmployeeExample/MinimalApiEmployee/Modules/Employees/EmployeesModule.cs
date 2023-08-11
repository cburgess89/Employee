using Data.Mediatr.Queries;
using Entities.DTO;
using MediatR;

namespace Modules.Employees
{
    public class EmployeesModule: IModule
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/Employees", async (IMediator _mediator) =>
            {
                var query = new GetAllEmployeesQuery();
                var result = await _mediator.Send(query);
                return Results.Ok(result);
            })
            .WithDisplayName("GetEmployees")
            .WithOpenApi();

            endpoints.MapGet("/Employees/{id}", async (int id, IMediator _mediator) =>
            {
                var query = new GetEmployeeByIdQuery(id);
                var result = await _mediator.Send(query);
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
            .WithDisplayName("GetEmployee")
            .WithOpenApi();

            endpoints.MapPost("/Employees", async (Employee_DTO_InsertUpdate command, IMediator _mediator) =>
            {
                var result = await _mediator.Send(command);
                return Results.Created($"/todoitems/{result.Id}", result);
            })
            .WithDisplayName("CreateEmployee")
            .WithOpenApi();

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }
    }
}
