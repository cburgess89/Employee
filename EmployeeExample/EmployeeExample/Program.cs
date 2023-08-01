using EmployeeExample;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Using custom Startup file just to keep things to a familiar, cleaner structure
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.Run();
