using RestWithASPNET.Business;
using RestWithASPNET.CrossCutting.Mapper;
using RestWithASPNET.CrossCutting.Security;
using RestWithASPNET.Repository;

namespace RestWithASPNET.CrossCutting.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPeopleBusiness, PeopleBusiness>();
            services.AddScoped<ILoginBusiness, LoginBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();

            services.AddTransient<ITokenService, TokenService>();

            services.AddAutoMapper(typeof(EntityToValueObject), typeof(ValueObjectToEntity));

            return services;
        }
    }
}
