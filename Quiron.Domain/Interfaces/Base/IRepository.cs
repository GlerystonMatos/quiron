using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Base
{
    public interface IRepository<TModel>
    {
        void Criar(TModel model);

        void Remover(TModel model);

        Task SalvarAlteracoes();

        ValueTask<TModel> PesquisarPorId(Guid id);

        IQueryable<TModel> ObterTodos();
    }
}