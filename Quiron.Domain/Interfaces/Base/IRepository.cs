using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Base
{
    public interface IRepository<TModel>
    {
        void Criar(TModel model);

        void Remover(TModel model);

        Task SalvarAlteracoesAsync();

        ValueTask<TModel> PesquisarPorIdAsync(Guid id);

        IQueryable<TModel> ObterTodos();
    }
}