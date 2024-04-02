using Microsoft.IdentityModel.Tokens;
using Quiron.Api.Configuracoes;
using Quiron.Domain.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quiron.Api.Security
{
    public static class AccessToken
    {
        public static string GenerateToken(UsuarioDto usuario, string tenant)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(TokenConfig.SecretKey);

            IList<Claim> claim = new List<Claim>();
            claim.Add(new Claim("tenant", tenant));
            claim.Add(new Claim(ClaimTypes.Name, usuario.Nome.ToString()));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddHours(TokenConfig.ExpireTimeInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}