using Microsoft.EntityFrameworkCore;
using Quiron.Data.Base;
using Quiron.Data.Context;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;
using System.Linq;

namespace Quiron.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(QuironContext context) : base(context)
        {
        }

        public Usuario PesquisarPorLoginSenha(string login, string senha)
            => _context.Set<Usuario>().Where(u => u.Login.ToUpper().Equals(login.ToUpper()) && u.Senha.ToUpper()
            .Equals(senha.ToUpper())).AsNoTracking().FirstOrDefault();
    }
}