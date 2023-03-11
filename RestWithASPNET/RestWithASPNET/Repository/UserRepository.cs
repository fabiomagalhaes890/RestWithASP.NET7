using Microsoft.EntityFrameworkCore;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;
using RestWithASPNET.Repository.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNET.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SQLContext sqlContext) : base(sqlContext) { }

        public User ValidateCredentials(UserValueObject user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _dataset.FirstOrDefault(u => u.UserName == user.UserName && u.Password == pass);
        }
        
        public User ValidateCredentials(string userName)
        {
            return _dataset.FirstOrDefault(u => u.UserName == userName);
        }
        
        public bool RevokeToken(string userName)
        {
            var user = _dataset.FirstOrDefault(u => u.UserName == userName);
            if (user == null) return false;

            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }
        
        public User RefreshUserInfo(User user)
        {
            if (!_dataset.Any(p => p.Id.Equals(user.Id))) return null;

            var result = _dataset.FirstOrDefault(p => p.Id == user.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }

            return result;
        }

        private string ComputeHash(string input, SHA256 algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes);

        }
    }
}
