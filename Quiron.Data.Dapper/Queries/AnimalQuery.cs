using Dapper;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Queries;
using System.Data.SqlClient;
using System.Text;

namespace Quiron.Data.Dapper.Queries
{
    public class AnimalQuery : IAnimalQuery
    {
        public IList<Animal> ObterTodosPorNome(string connectionString, string nome)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("SELECT Id,");
                stringBuilder.Append("       Nome");
                stringBuilder.Append("  FROM Animal");
                stringBuilder.Append(" WHERE Nome LIKE @Nome");

                var parametros = new { Nome = $"%{nome}%" };

                IEnumerable<Animal> animais = sqlConnection.Query<Animal>(stringBuilder.ToString(), parametros);
                return animais.ToList();
            }
        }
    }
}