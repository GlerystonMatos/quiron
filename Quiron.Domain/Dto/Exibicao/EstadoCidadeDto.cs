using Quiron.Domain.Dto.Base;
using System;
using System.ComponentModel;

namespace Quiron.Domain.Dto.Exibicao
{
    [DisplayName("EstadoCidade")]
    public class EstadoCidadeDto : BaseDto
    {
        public EstadoCidadeDto()
            => Id = Guid.NewGuid();

        public string Nome { get; set; }

        public string Uf { get; set; }
    }
}