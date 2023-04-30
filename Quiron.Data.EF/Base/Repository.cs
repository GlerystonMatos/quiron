using Microsoft.EntityFrameworkCore;
using Quiron.Data.EF.Context;
using Quiron.Domain.Entities.Base;
using Quiron.Domain.Interfaces.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Data.EF.Base
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : Entity
    {
        protected readonly QuironContext _context;

        public Repository(QuironContext context)
            => _context = context;

        public void Criar(TModel model)
            => _context.Set<TModel>().Add(model);

        public void Remover(TModel model)
            => _context.Set<TModel>().Remove(model);

        public Task SalvarAlteracoes()
            => _context.SaveChangesAsync();

        public ValueTask<TModel> PesquisarPorId(Guid id)
            => _context.Set<TModel>().FindAsync(id);

        public IQueryable<TModel> ObterTodos()
            => _context.Set<TModel>().AsNoTracking().AsQueryable();
    }
}