using Quiron.Domain.Dto.Base;
using Quiron.Domain.Dto.Exibicao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quiron.Domain.Dto
{
    [DisplayName("Estado")]
    public class EstadoDto : BaseDto
    {
        public EstadoDto()
            => Id = Guid.NewGuid();

        [Required(ErrorMessage = "Campo 'Nome' obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo 'Uf' obrigatório")]
        public string Uf { get; set; }

        public ICollection<CidadeEstadoDto> Cidades { get; set; }
    }
}