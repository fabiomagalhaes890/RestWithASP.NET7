using AutoMapper;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;

namespace RestWithASPNET.CrossCutting.Mapper
{
    public class ValueObjectToEntity : Profile
    {
        public ValueObjectToEntity() 
        {
            CreateMap<PeopleValueObject, People>();
            CreateMap<UserValueObject, User>()
                .ForMember(u => u.RefreshToken, opt => opt.Ignore())
                .ForMember(u => u.RefreshTokenExpiryTime, opt => opt.Ignore());
        }
    }
}
