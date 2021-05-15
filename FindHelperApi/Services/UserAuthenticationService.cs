using FindHelperApi.Data;
using FindHelperApi.Models;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

namespace FindHelperApi.Services
{
    public class UserAuthenticationService
    {
        private readonly FindHelperApiContext _context;

        public UserAuthenticationService(FindHelperApiContext context)
        {
            _context = context;
        }

        public User Login(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            bool isValidPassword = BC.Verify(password, user.Password);
            
            if (isValidPassword)
                return user;

            return null;
        }
    }
}
