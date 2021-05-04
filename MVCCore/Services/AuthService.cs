using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVCCore.DbContext;
using MVCCore.Models;
using MVCCore.Options;


namespace MVCCore.Services
{
    public class AuthService: ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly string _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
        
        public AuthService(SchoolContext context)
        {
            _context = context;
        }
        
        public async Task<AuthenticationResult> Login(string username, string password)
        {
            var encPassword = Utils.EncryptData(password);
            //var decPassword = Utils.Decryptdata(encPassword);
            User user =  _context.Users.SingleOrDefault(m => m.UserName == username && m.Password == encPassword);
            if (user == null)
            {
                return null;
            }
            return GenerateToken(user);
        }
        
        private AuthenticationResult GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, value: user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, value: user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}