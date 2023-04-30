using Quiron.Domain.Dto;
using Quiron.Domain.Interfaces.Base;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Services
{
    public interface IEstadoService : IService<EstadoDto>
    {
        Task<EstadoDto[]> ObterTodosPorUf(string uf);
    }
}