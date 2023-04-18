using Quiron.Domain.Dto.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quiron.Domain.Dto
{
    [DisplayName("Usuario")]
    public class UsuarioDto : BaseDto
    {
        public UsuarioDto()
            => Id = Guid.NewGuid();

        [Required(ErrorMessage = "Campo 'Nome' obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo 'Login' obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo 'Senha' obrigatório")]
        public string Senha { get; set; }
    }
}