using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Base;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Data
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> PesquisarPorLoginSenhaAsync(string login, string senha);
    }
}