using AutoMapper;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;

namespace RestWithASPNET.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserBusiness(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public UserValueObject Create(UserValueObject user)
        {
            var entity = _mapper.Map<User>(user);
            var result = _repository.Create(entity);
            return _mapper.Map<UserValueObject>(result);
        }
    }
}
