using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Base
{
    public interface IService<TModel>
    {
        void Criar(TModel model);

        void Atualizar(TModel model);

        void Remover(Guid id);

        Task SalvarAlteracoes();

        Task<TModel> PesquisarPorId(Guid id);

        IQueryable<TModel> ObterTodos();
    }
}