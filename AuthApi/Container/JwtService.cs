using AuthApi.Data;
using AuthApi.Dtos;
using AuthApi.Models;
using AuthApi.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.Container
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly MyDbContext _context;
        public JwtService(IConfiguration configuration , MyDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> generateRefreshTokenAsync(int userId)
        {
            var randomNumber = new Byte[32];
            using(var randGenerator = RandomNumberGenerator.Create())
            {
                randGenerator.GetBytes(randomNumber);
                string refToken = Convert.ToBase64String(randomNumber);
                var existToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Id == userId);
                if(existToken != null)
                {
                    existToken.RefreshTokenId = refToken;
                }
                else
                {
                    await _context.RefreshTokens.AddAsync(new RefreshToken()
                    {
                        Id = userId,
                        RefreshTokenId = refToken,
                        TokenId = new Random().Next().ToString()
                    }
                    );
                }

                await _context.SaveChangesAsync();

                return refToken;
            }
        }

        public SecurityToken generateToken(User user)
        {
            var secretKey = _configuration.GetSection("JwtSetting:JwtKey").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(secretKey));

            var claims = new Claim[]
            {
                 new Claim(ClaimTypes.Name,user.Id.ToString()),
                 new Claim(ClaimTypes.NameIdentifier,user.Name),
                 new Claim(ClaimTypes.Role, user.Role)
                 
            };

            var signingCredintiels = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredintiels,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return token;
        }

        public async Task<string> generateNewTokenAsync(UserLoginResDto model)
        {
            var secretKey = _configuration.GetSection("JwtSetting:JwtKey").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(secretKey));

           
            
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(model.Token, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = key
            }
            , out securityToken
            );
            var token = securityToken as JwtSecurityToken;
            if(token != null && token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                string userId =  principal.Identity?.Name;
                var exist = await _context.RefreshTokens.AnyAsync(x => x.RefreshTokenId == model.RefreshToken &&
                x.Id.ToString() == userId);
                if(exist)
                {
                    var newToken = new JwtSecurityToken(

                       claims: principal.Claims.ToArray(),
                       expires:DateTime.Now.AddSeconds(30),
                       signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                       );
                    var finalToken = tokenHandler.WriteToken(newToken);
                    model.Token = finalToken;
                    model.RefreshToken = await generateRefreshTokenAsync(Convert.ToInt32(userId));
                    return finalToken;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
