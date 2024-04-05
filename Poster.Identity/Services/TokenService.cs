using Microsoft.IdentityModel.Tokens;
using Poster.Identity.Interfaces;
using Poster.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Poster.Identity.Services
{
    public class TokenService : ITokenService
    {
        public static readonly string key = "!my_own#app%super_secret^key_nigga";

        public static readonly string audience = "Poster Users";

        public static readonly string issuer = "Denis S.";

        public string GetToken(TokenModel tokenModel)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, tokenModel.UserName),

                new Claim(ClaimTypes.Email, tokenModel.UserEmail)
            };

            ClaimsIdentity identity = 
                new ClaimsIdentity(claims, "Bearer", ClaimTypes.Name, ClaimTypes.Role);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            JwtSecurityToken securityToken = handler.CreateJwtSecurityToken
                (
                    subject: identity,
                    signingCredentials: signingCredentials,
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.Now.AddHours(2)
                );

            return handler.WriteToken(securityToken);
        }

        public bool ValidateToken(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler ();

            try
            {
                handler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidIssuer = issuer,
                    ValidAudience = audience

                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
