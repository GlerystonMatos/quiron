using Quiron.Domain.Entities;
using System.Collections.Generic;

namespace Quiron.Domain.Interfaces.Queries
{
    public interface IAnimalQuery
    {
        IList<Animal> ObterTodosPorNome(string connectionString, string nome);
    }
}