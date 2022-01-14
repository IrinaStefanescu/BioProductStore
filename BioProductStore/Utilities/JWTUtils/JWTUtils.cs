using BioProductStore.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BioProductStore.Utilities.JWTUtils
{
    public class JWTUtils : IJWTUtils
    {
        private readonly AppSettings _appSettings;

        public JWTUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateJWTToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); //cu tokenHandler asta generam noi chestii
            var appPrivateKey = Encoding.ASCII.GetBytes(_appSettings.JwtSecret); //cheia asta appPrivateKey o folosim la hash-uire si o encodam

            var tokenDescriptor =
                new
                    SecurityTokenDescriptor //descriem cum o sa functioneze tokenul nostru, cum o sa se genereze, ce hashing algoritm o sa foloseasca, ce o sa contina
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }), //aici o sa punem id ul userului
                    Expires = DateTime.UtcNow.AddDays(10), //cand expira tokenul
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(appPrivateKey),
                            SecurityAlgorithms
                                .HmacSha256) //aici zicem algoritmii pe care ii foloseste adica o sa foloseasca acest appPrivateKey al nostru pt encodare iar algoritmul este HmacSha256
                };
            var token = tokenHandler.CreateToken(tokenDescriptor); //generam tokenul
            return tokenHandler.WriteToken(token);
        }

        public Guid ValidateJWTToken(string token)
        {
            if (token == null)
                return Guid.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var appPrivateKey = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(appPrivateKey), //o sa folosesc aceasta cheie pt a valida tokenul
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                Console.WriteLine(validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken; //facem parse la jwtToken
                var userId = new Guid(jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value);

                return userId;
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
        }

    }
}
