using RestWithASPNET.Business;
using RestWithASPNET.CrossCutting.Mapper;
using RestWithASPNET.CrossCutting.Security;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.CrossCutting.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<People>, Repository<People>>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPeopleBusiness, PeopleBusiness>();
            services.AddScoped<ILoginBusiness, LoginBusiness>();

            services.AddTransient<ITokenService, TokenService>();

            services.AddAutoMapper(typeof(EntityToValueObject), typeof(ValueObjectToEntity));

            return services;
        }
    }
}
