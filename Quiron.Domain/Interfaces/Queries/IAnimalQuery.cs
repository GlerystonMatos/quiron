using Quiron.Domain.Entities;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Queries
{
    public interface IAnimalQuery
    {
        Task<Animal[]> ObterTodosPorNome(string connectionString, string nome);
    }
}