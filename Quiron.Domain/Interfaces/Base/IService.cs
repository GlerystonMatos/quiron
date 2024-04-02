using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Base
{
    public interface IService<TModel>
    {
        void Criar(TModel model);

        Task AtualizarAsync(TModel model);

        Task RemoverAsync(Guid id);

        Task SalvarAlteracoesAsync();

        Task<TModel> PesquisarPorIdAsync(Guid id);

        IQueryable<TModel> ObterTodos();
    }
}