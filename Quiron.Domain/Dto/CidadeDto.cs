using Quiron.Domain.Dto.Base;
using Quiron.Domain.Dto.Exibicao;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quiron.Domain.Dto
{
    [DisplayName("Cidade")]
    public class CidadeDto : BaseDto
    {
        public CidadeDto()
            => Id = Guid.NewGuid();

        [Required(ErrorMessage = "Campo 'Nome' obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo 'IdEstado' obrigatório")]
        public Guid IdEstado { get; set; }

        public EstadoCidadeDto Estado { get; set; }
    }
}