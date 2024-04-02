using Quiron.Domain.Dto;
using Quiron.Domain.Interfaces.Base;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Services
{
    public interface ICidadeService : IService<CidadeDto>
    {
        Task<CidadeDto[]> ObterTodosPorNomeAsync(string nome);
    }
}