using Quiron.Domain.Entities;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Queries
{
    public interface IEstadoQuery
    {
        Task<Estado[]> ObterTodosPorUf(string connectionString, string uf);
    }
}