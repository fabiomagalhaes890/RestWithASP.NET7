using RestWithASPNET.Services.People;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Sempre abaixo do addControllers, Dependency injection
builder.Services.AddScoped<IPeopleService, PeopleService>();

//colocar para rodar o versionamento de API
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
