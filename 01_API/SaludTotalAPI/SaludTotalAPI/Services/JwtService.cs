using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SaludTotalAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SaludTotalAPI.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        // ✅ GENERAR TOKEN CON CLAIMS PERSONALIZADOS (Ej: QR)
        public string GenerateToken(Dictionary<string, string> claims, TimeSpan expiracion)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.Select(kv => new Claim(kv.Key, kv.Value))),
                Expires = DateTime.UtcNow.Add(expiracion),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // ✅ GENERAR TOKEN PARA USUARIO AUTENTICADO
        public string GenerateToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim("id", usuario.IdUsuario.ToString()),
                new Claim("nombreUsuario", usuario.NombreUsuario),
                new Claim("email", usuario.Email ?? ""),
                new Claim(ClaimTypes.Role, usuario.NombreUsuario.ToLower().Contains("admin") ? "Admin" : "Usuario")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ✅ VALIDAR TOKEN Y EXTRAER LOS CLAIMS (Ej: en confirmación de QR)
        public Dictionary<string, string>? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _config["JwtSettings:Issuer"],
                    ValidAudience = _config["JwtSettings:Audience"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);
            }
            catch
            {
                return null;
            }
        }

        internal string GenerarToken(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}

