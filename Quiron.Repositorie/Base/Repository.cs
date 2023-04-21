using Microsoft.EntityFrameworkCore;
using Quiron.Data.EF.Context;
using Quiron.Domain.Entities.Base;
using Quiron.Domain.Interfaces.Base;
using System;
using System.Linq;

namespace Quiron.Data.EF.Base
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : Entity
    {
        protected readonly QuironContext _context;

        public Repository(QuironContext context)
            => _context = context;

        public void Criar(TModel model)
        {
            _context.Set<TModel>().Add(model);
            _context.SaveChanges();
        }

        public void Atualizar(TModel model)
        {
            _context.Set<TModel>().Update(model);
            _context.SaveChanges();
        }

        public void Remover(TModel model)
        {
            _context.Set<TModel>().Remove(model);
            _context.SaveChanges();
        }

        public TModel PesquisarPorId(Guid id)
            => _context.Set<TModel>().Where(t => t.Id.Equals(id)).AsNoTracking().FirstOrDefault();

        public virtual IQueryable<TModel> ObterTodos()
            => _context.Set<TModel>().AsNoTracking().AsQueryable();
    }
}