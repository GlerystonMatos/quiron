using Dapper;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Queries;
using System.Data.SqlClient;
using System.Text;

namespace Quiron.Data.Dapper.Queries
{
    public class EstadoQuery : IEstadoQuery
    {
        public async Task<Estado[]> ObterTodosPorUf(string connectionString, string uf)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT e.Id, ");
                sql.Append("       e.Nome, ");
                sql.Append("       e.Uf, ");
                sql.Append("       c.Id, ");
                sql.Append("       c.Nome, ");
                sql.Append("       c.IdEstado ");
                sql.Append("  FROM Estado e ");
                sql.Append(" INNER JOIN Cidade c ON c.IdEstado = e.Id ");
                sql.Append(" WHERE e.Uf = @Uf");

                var parametros = new { Uf = uf };

                IEnumerable<Estado> estados = await connection.QueryAsync<Estado, Cidade, Estado>(
                    sql.ToString(),
                    (estado, cidade) =>
                    {
                        estado.Cidades = estado.Cidades ?? new List<Cidade>();
                        estado.Cidades.Add(cidade);
                        return estado;
                    },
                    parametros,
                    splitOn: "Id"
                );

                return estados
                    .GroupBy(e => e.Id)
                    .Select(g => new Estado
                    {
                        Id = g.Key,
                        Nome = g.First().Nome,
                        Uf = g.First().Uf,
                        Cidades = g.SelectMany(e => e.Cidades).ToList()
                    })
                    .ToArray();
            }
        }
    }
}