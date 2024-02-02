using raycommerceproject.Data;
using raycommerceproject.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace raycommerceproject.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        
        public UserService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }
        







        public void RegisterUser(User user)
        {
            if (_dbContext.Users.Any(u => u.Email == user.Email))
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            using var hmac = new HMACSHA256();
            var key = hmac.Key;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),

            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User AuthenticateUser(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }
    }
}
