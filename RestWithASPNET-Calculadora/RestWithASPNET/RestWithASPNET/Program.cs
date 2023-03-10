using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestWithASPNET.Business;
using RestWithASPNET.CrossCutting.Configurations;
using RestWithASPNET.CrossCutting.Extensions;
using RestWithASPNET.CrossCutting.Hypermedia.Enricher;
using RestWithASPNET.CrossCutting.Hypermedia.Filters;
using RestWithASPNET.CrossCutting.Mapper;
using RestWithASPNET.Models;
using RestWithASPNET.Repository.Generic;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// colocar os valores do appsettings no tokenconfigurations
var tokenConfigurations = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(
    builder.Configuration.GetSection("TokenConfigurations"))
    .Configure(tokenConfigurations);
builder.Services.AddSingleton(tokenConfigurations);
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfigurations.Issuer,
            ValidAudience = tokenConfigurations.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
        };
    });

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddControllers();

// Sempre abaixo do addControllers, Dependency injection
builder.Services.RegisterServices();

// Registrar dbcontext para acesso ao banco de dados
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<SQLContext>(options => options.UseSqlServer(connectionString));

// dependency injection do hypermedia
var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentReponseEnricherList.Add(new PeopleEnricher());

builder.Services.AddSingleton(filterOptions);

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml").ToString());
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json").ToString());
}).AddXmlSerializerFormatters();

//colocar para rodar o versionamento de API
builder.Services.AddApiVersioning();

// configuracao do cabeçalho do swagger json
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

// cors deve ficar depois de httpsredirection e userouting e antes de mapcontrollers
app.UseCors();

// gera json com documentacao
app.UseSwagger();
// gera pag html acessivel 
app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest Api"); });

//Configura redirecionamento para swagger
var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

// Configuracao para controle de versao e hateoas
app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=value}/{id?}");

app.Run();
