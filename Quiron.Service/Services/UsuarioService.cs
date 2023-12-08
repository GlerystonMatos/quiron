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

        public async void Atualizar(UsuarioDto origem)
        {
            Usuario destino = await _usuarioRepository.PesquisarPorId(origem.Id);
            _mapper.Map(origem, destino);
        }

        public async void Remover(Guid id)
        {
            Usuario usuario = await _usuarioRepository.PesquisarPorId(id);
            _usuarioRepository.Remover(usuario);
        }

        public Task SalvarAlteracoes()
            => _usuarioRepository.SalvarAlteracoes();

        public async Task<UsuarioDto> PesquisarPorId(Guid id)
            => _mapper.Map<UsuarioDto>(await _usuarioRepository.PesquisarPorId(id));

        public IQueryable<UsuarioDto> ObterTodos()
            => _usuarioRepository.ObterTodos().ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider);

        private async Task<UsuarioDto> PesquisarPorLoginSenha(string login, string senha)
            => _mapper.Map<UsuarioDto>(await _usuarioRepository.PesquisarPorLoginSenha(login, senha));

        public async Task<UsuarioDto> ObterUsuarioParaAutenticacao(LoginDto login)
        {
            UsuarioDto usuario = await PesquisarPorLoginSenha(login.Login, login.Senha);

            if (usuario == null)
                throw new QuironException("Usuário não localizado.");

            return usuario;
        }
    }
}