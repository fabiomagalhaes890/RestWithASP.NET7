using Microsoft.EntityFrameworkCore;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNET.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLContext _context;
        private DbSet<User> _dataset;

        public UserRepository(SQLContext context)
        {
            _context = context;
            _dataset = _context.Set<User>();
        }

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
