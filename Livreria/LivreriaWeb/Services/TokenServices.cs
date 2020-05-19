using LivreriaWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LivreriaWeb.Services
{
    public static class TokenServices
    {
        public static string GenerarateToken (IdentityUser user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
           
            var tokenDescriptor = new SecurityTokenDescriptor
            { 
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                
                }),

                Expires = DateTime.UtcNow.AddHours(2),

            SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(key),
               SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);

            return tokenhandler.WriteToken(token);


        }
    }
}
