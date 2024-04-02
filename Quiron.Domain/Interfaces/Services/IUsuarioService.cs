using Quiron.Domain.Dto;
using Quiron.Domain.Interfaces.Base;
using System.Threading.Tasks;

namespace Quiron.Domain.Interfaces.Services
{
    public interface IUsuarioService : IService<UsuarioDto>
    {
        Task<UsuarioDto> ObterUsuarioParaAutenticacaoAsync(LoginDto login);
    }
}