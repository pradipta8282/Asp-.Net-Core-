using AspdotnetcoreCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var provider = builder.Services.BuildServiceProvider();
//created the service.builder.Services is the service collection where all dependencies (like DbContext, IConfiguration, etc.) are registered.
//.BuildServiceProvider() creates an instance of ServiceProvider, which allows us to manually retrieve services from the dependency injection (DI) container.


var config = provider.GetRequiredService<IConfiguration>();
//using the provider variable done the iconfig setting.provider.GetRequiredService<IConfiguration>() fetches the IConfiguration service, which is responsible for handling application configurations (like reading from appsettings.json).
//This line ensures that config holds the application's configuration settings.



builder.Services.AddDbContext<mydbContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));
////dbcontext and sql server which is db provider need to be register here.builder.Services.AddDbContext<mydbContext>() registers mydbContext with the DI container so it can be injected where needed.
//item.UseSqlServer(config.GetConnectionString("dbcs")):
//config.GetConnectionString("dbcs") retrieves the connection string named "dbcs" from appsettings.json.
//UseSqlServer() tells Entity Framework Core to use SQL Server as the database provider.

//Why are these lines used?
//The first two lines manually fetch IConfiguration because services like DbContext need the database connection string, which is inside IConfiguration.
//Then, the DbContext is registered using the retrieved connection string.
//This allows the application to use Entity Framework Core with SQL Server.



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
