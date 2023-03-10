using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestWithASPNET.Business;
using RestWithASPNET.CrossCutting.Hypermedia.Enricher;
using RestWithASPNET.CrossCutting.Hypermedia.Filters;
using RestWithASPNET.CrossCutting.Mapper;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Repository.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Sempre abaixo do addControllers, Dependency injection
builder.Services.AddScoped<IRepository<People>, Repository<People>>();
builder.Services.AddScoped<IPeopleBusiness, PeopleBusiness>();

builder.Services.AddAutoMapper(typeof(EntityToValueObject), typeof(ValueObjectToEntity));

// Registrar dbcontext para acesso ao banco de dados
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<SQLContext>(options => options.UseSqlServer(connectionString));

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentReponseEnricherList.Add(new PeopleEnricher());

builder.Services.AddSingleton(filterOptions);

//colocar para rodar o versionamento de API
builder.Services.AddApiVersioning();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Rest Api",
            Version = "v1",
            Description = "Description",
            Contact = new OpenApiContact
            {
                Name = "Fabio Magalhães",
                Url = new Uri("https://github.com/fabiomagalhaes890")
            }
        });
});

var app = builder.Build();

// executa as migrations geradas pelo ef
using (var scope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<SQLContext>().Database.Migrate();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger(); // gera json com documentacao
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest Api");
}); // gera pag html acessivel 

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=value}/{id?}");

app.Run();
