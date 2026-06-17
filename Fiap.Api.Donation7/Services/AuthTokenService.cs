using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiap.Api.Donation7.Services
{
    public class AuthTokenService(IConfiguration configuration)
    {

        private readonly IConfiguration _configuration = configuration; 


        public string GenerateToken(string email, int id, string roles)
        {

            // claims - > informações do usuário, como email, id, roles, etc
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.NameId, id.ToString()),
                new Claim(ClaimTypes.Role, roles),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Jti é um identificador único para o token
            };

            // key - > chave secreta para assinar o token, garantindo que ele não seja alterado
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // credienciais - > combinação da chave e do algoritmo de assinatura
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // token - > objeto que representa o token JWT, contendo as claims, a data de expiração e as credenciais
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(3),
                claims: claims,
                signingCredentials: creds
            );
        

            // retorno token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
