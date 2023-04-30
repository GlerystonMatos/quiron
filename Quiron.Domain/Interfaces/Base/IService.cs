using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Base
{
    public interface IService<TModel>
    {
        Task Criar(TModel model);

        Task Atualizar(TModel model);

        Task Remover(Guid id);

        Task<TModel> PesquisarPorId(Guid id);

        IQueryable<TModel> ObterTodos();
    }
}