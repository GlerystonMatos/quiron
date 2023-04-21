using Quiron.Domain.Dto.Base;
using System;
using System.ComponentModel;

namespace Quiron.Domain.Dto.Exibicao
{
    [DisplayName("CidadeEstado")]
    public class CidadeEstadoDto : BaseDto
    {
        public CidadeEstadoDto()
            => Id = Guid.NewGuid();

        public string Nome { get; set; }
    }
}