using Quiron.Domain.Entities;
using System.Collections.Generic;

namespace Quiron.Domain.Interfaces.Queries
{
    public interface IEstadoQuery
    {
        IList<Estado> ObterTodosPorUf(string connectionString, string uf);
    }
}