using Quiron.Domain.Entities;
using System.Collections.Generic;

namespace Quiron.Domain.Interfaces.Queries
{
    public interface ICidadeQuery
    {
        IList<Cidade> ObterTodosPorNome(string connectionString, string nome);
    }
}