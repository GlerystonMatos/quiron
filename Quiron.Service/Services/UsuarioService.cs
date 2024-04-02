using AutoMapper;
using AutoMapper.QueryableExtensions;
using Quiron.Domain.Dto;
using Quiron.Domain.Entities;
using Quiron.Domain.Exception;
using Quiron.Domain.Interfaces.Data;
using Quiron.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public void Criar(UsuarioDto usuario)
            => _usuarioRepository.Criar(_mapper.Map<Usuario>(usuario));

        public async Task AtualizarAsync(UsuarioDto origem)
        {
            Usuario destino = await _usuarioRepository.PesquisarPorIdAsync(origem.Id);
            _mapper.Map(origem, destino);
        }

        public async Task RemoverAsync(Guid id)
        {
            Usuario usuario = await _usuarioRepository.PesquisarPorIdAsync(id);
            _usuarioRepository.Remover(usuario);
        }

        public Task SalvarAlteracoesAsync()
            => _usuarioRepository.SalvarAlteracoesAsync();

        public async Task<UsuarioDto> PesquisarPorIdAsync(Guid id)
            => _mapper.Map<UsuarioDto>(await _usuarioRepository.PesquisarPorIdAsync(id));

        public IQueryable<UsuarioDto> ObterTodos()
            => _usuarioRepository.ObterTodos().ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider);

        private async Task<UsuarioDto> PesquisarPorLoginSenhaAsync(string login, string senha)
            => _mapper.Map<UsuarioDto>(await _usuarioRepository.PesquisarPorLoginSenhaAsync(login, senha));

        public async Task<UsuarioDto> ObterUsuarioParaAutenticacaoAsync(LoginDto login)
        {
            UsuarioDto usuario = await PesquisarPorLoginSenhaAsync(login.Login, login.Senha);

            if (usuario == null)
                throw new QuironException("Usuário não localizado.");

            return usuario;
        }
    }
}