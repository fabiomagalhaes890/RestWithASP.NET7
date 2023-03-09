using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Business;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Sempre abaixo do addControllers, Dependency injection
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IPeopleBusiness, PeopleBusiness>();

// Registrar dbcontext para acesso ao banco de dados
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<SQLContext>(options => options.UseSqlServer(connectionString));

//colocar para rodar o versionamento de API
builder.Services.AddApiVersioning();

var app = builder.Build();

using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetRequiredService<SQLContext>().Database.Migrate();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
