using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Base;

namespace Quiron.Domain.Interfaces.Data
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario PesquisarPorLoginSenha(string login, string senha);
    }
}