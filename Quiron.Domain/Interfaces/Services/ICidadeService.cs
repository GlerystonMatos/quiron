using Quiron.Domain.Dto;
using Quiron.Domain.Interfaces.Base;
using System.Collections.Generic;

namespace Quiron.Domain.Interfaces.Services
{
    public interface ICidadeService : IService<CidadeDto>
    {
        IList<CidadeDto> ObterTodosPorNome(string nome);
    }
}