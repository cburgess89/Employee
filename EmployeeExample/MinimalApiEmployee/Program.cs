using Entities.AutoMapper;
using Data.Repositories;
using Data;
using Microsoft.EntityFrameworkCore;
using Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutomapperProfile));

//Database context, would swap this out with an environment based SQL connection from appsettings
builder.Services.AddDbContext<SolutionDatabaseContext>(opt => opt.UseInMemoryDatabase(databaseName: "Example"));

//Repositories -------------------------------------------------------------------------------
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
//End of Repositories ------------------------------------------------------------------------

builder.Services.RegisterModules();

builder.Services.AddMediatR(config =>
{
    //Register all Mediatr handlers from any project
    config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

//Create scope and seed Employee data ------------------------------------------------------------
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
    dbInitializer.SeedData();
}
//End of Create scope and seed Employee data ------------------------------------------------------

#region Minimal API Endpoints

app.MapEndpoints();

#endregion Minimal API Endpoints

app.Run();