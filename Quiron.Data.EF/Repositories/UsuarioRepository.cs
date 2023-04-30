using Microsoft.EntityFrameworkCore;
using Quiron.Data.EF.Base;
using Quiron.Data.EF.Context;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Data.EF.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(QuironContext context) : base(context)
        {
        }

        public Task<Usuario> PesquisarPorLoginSenha(string login, string senha)
            => _context.Set<Usuario>().Where(u =>
                u.Login.ToUpper().Equals(login.ToUpper()) &&
                u.Senha.ToUpper().Equals(senha.ToUpper()))
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}