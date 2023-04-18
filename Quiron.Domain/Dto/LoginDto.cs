using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quiron.Domain.Dto
{
    [DisplayName("Login")]
    public class LoginDto
    {
        [Required(ErrorMessage = "Campo 'Login' obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo 'Senha' obrigatório")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo 'Tenant' obrigatório")]
        public string Tenant { get; set; }
    }
}