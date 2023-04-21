using Quiron.Domain.Dto;
using Quiron.Domain.Interfaces.Base;
using System.Collections.Generic;

namespace Quiron.Domain.Interfaces.Services
{
    public interface IEstadoService : IService<EstadoDto>
    {
        IList<EstadoDto> ObterTodosPorUf(string uf);
    }
}