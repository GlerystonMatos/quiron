using Quiron.Domain.Dto;
using Quiron.Domain.Interfaces.Base;
using System.Collections.Generic;

namespace Quiron.Domain.Interfaces.Services
{
    public interface IAnimalService : IService<AnimalDto>
    {
        IList<AnimalDto> ObterTodosPorNome(string nome);
    }
}