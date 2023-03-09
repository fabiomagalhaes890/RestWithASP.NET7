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
        }
    }
}
