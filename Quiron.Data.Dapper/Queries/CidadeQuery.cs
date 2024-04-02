using Dapper;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Queries;
using System.Data.SqlClient;
using System.Text;

namespace Quiron.Data.Dapper.Queries
{
    public class CidadeQuery : ICidadeQuery
    {
        public async Task<Cidade[]> ObterTodosPorNomeAsync(string connectionString, string nome)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT c.Id, ");
                sql.Append("       c.Nome, ");
                sql.Append("       c.IdEstado, ");
                sql.Append("       e.Id, ");
                sql.Append("       e.Nome, ");
                sql.Append("       e.Uf ");
                sql.Append("  FROM Cidade c ");
                sql.Append(" INNER JOIN Estado e ON e.Id = c.IdEstado ");
                sql.Append(" WHERE c.Nome LIKE @Nome");

                var parametros = new { Nome = $"%{nome}%" };

                IEnumerable<Cidade> cidades = await connection.QueryAsync<Cidade, Estado, Cidade>(
                    sql.ToString(),
                    (cidade, estado) =>
                    {
                        cidade.Estado = estado;
                        return cidade;
                    },
                    parametros,
                    splitOn: "Id, Id"
                );

                return cidades.ToArray();
            }
        }
    }
}