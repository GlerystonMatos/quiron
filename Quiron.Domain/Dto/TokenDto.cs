using System.ComponentModel;

namespace Quiron.Domain.Dto
{
    [DisplayName("Token")]
    public class TokenDto
    {
        public TokenDto(string token)
            => Token = token;

        public string Token { get; set; }
    }
}