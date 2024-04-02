using Quiron.Domain.Entities;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Queries
{
    public interface ICidadeQuery
    {
        Task<Cidade[]> ObterTodosPorNomeAsync(string connectionString, string nome);
    }
}