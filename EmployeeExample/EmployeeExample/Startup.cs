using Entities.AutoMapper;
using Microsoft.AspNetCore.Mvc.Versioning;
using Newtonsoft.Json.Serialization;
using Data.Repositories;
using Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;

namespace EmployeeExample
{
    public class Startup
    {
        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt => {

            }).AddNewtonsoftJson(options => {
                //following pattern of lower case first, then camel case after
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(AutomapperProfile));

            //Database context, would swap this out with an environment based SQL connection from appsettings
            services.AddDbContext<SolutionDatabaseContext>(opt => opt.UseInMemoryDatabase(databaseName: "Example"));

            //Repositories -------------------------------------------------------------------------------
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            //End of Repositories ------------------------------------------------------------------------

            //Versioning
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new QueryStringApiVersionReader("version");
            });

            services.AddMediatR(config =>
            {
                //Register all Mediatr handlers from any project
                config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            
            app.UseHttpsRedirection();

            //Create scope and seed Employee data ------------------------------------------------------------
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                dbInitializer.SeedData();
            }
            //End of Create scope and seed Employee data ------------------------------------------------------

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(x => x.MapControllers());            
        }
    }
}
