using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Models.Services;

public class TokenService
{
    public static string GenerateToken(UserModel userModel)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var criptoKey = Encoding.ASCII.GetBytes(JwtKey.secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, userModel.Id.ToString()),
                new Claim(ClaimTypes.Name, userModel.Name)
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(criptoKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}