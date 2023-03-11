using AutoMapper;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;

namespace RestWithASPNET.CrossCutting.Mapper
{
    public class EntityToValueObject : Profile
    {
        public EntityToValueObject() 
        {
            CreateMap<People, PeopleValueObject>();
            CreateMap<User, UserValueObject>();
        }
    }
}
