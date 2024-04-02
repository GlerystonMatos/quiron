using Quiron.Domain.Dto;
using Quiron.Domain.Interfaces.Base;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Services
{
    public interface IAnimalService : IService<AnimalDto>
    {
        Task<AnimalDto[]> ObterTodosPorNomeAsync(string nome);
    }
}