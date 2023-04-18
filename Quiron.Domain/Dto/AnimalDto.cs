using Quiron.Domain.Dto.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quiron.Domain.Dto
{
    [DisplayName("Animal")]
    public class AnimalDto : BaseDto
    {
        public AnimalDto()
            => Id = Guid.NewGuid();

        [Required(ErrorMessage = "Campo 'Nome' obrigatório")]
        public string Nome { get; set; }
    }
}