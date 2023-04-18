using System;
using System.Linq;

namespace Quiron.Domain.Interfaces.Base
{
    public interface IService<TModel>
    {
        void Criar(TModel model);

        void Atualizar(TModel model);

        void Remover(TModel model);

        TModel PesquisarPorId(Guid id);

        IQueryable<TModel> ObterTodos();
    }
}