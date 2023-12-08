using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Quiron.Domain.Dto;
using Quiron.Domain.Entities;
using Quiron.Domain.Exception;
using Quiron.Domain.Interfaces.Data;
using Quiron.Domain.Interfaces.Services;
using Quiron.NUnitTest.Utilitarios;
using Quiron.Service.Services;

namespace Quiron.NUnitTest.Services
{
    public class UsuarioServiceTest
    {
        private IMapper _mapper;
        private IUsuarioService _usuarioService;
        private Mock<IUsuarioRepository> _usuarioRepository;

        public UsuarioServiceTest()
        {
            _mapper = Mapeador.Get();
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_mapper, _usuarioRepository.Object);
        }

        [Test]
        public void CriarTest()
        {
            UsuarioDto usuario = new UsuarioDto();
            ClassicAssert.DoesNotThrow(() => _usuarioService.Criar(usuario));
        }

        [Test]
        public void AtualizarTest()
        {
            UsuarioDto usuario = new UsuarioDto();
            ClassicAssert.DoesNotThrow(() => _usuarioService.Atualizar(usuario));
        }

        [Test]
        public void RemoverTest()
            => ClassicAssert.DoesNotThrow(() => _usuarioService.Remover(Guid.NewGuid()));

        [Test]
        public void ObterTodosTest()
        {
            Usuario usuario01 = new Usuario(Guid.NewGuid(), "Teste 03", "Teste 03", "Teste03");
            Usuario usuario02 = new Usuario(Guid.NewGuid(), "Teste 04", "Teste 04", "Teste04");

            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuario01);
            usuarios.Add(usuario02);

            _usuarioRepository.Setup(r => r.ObterTodos()).Returns(usuarios.AsQueryable());
            ClassicAssert.IsNotNull(_usuarioService.ObterTodos());
        }

        [Test]
        public void PesquisarPorIdTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 05", "Teste 05", "Teste05");

            _usuarioRepository.Setup(r => r.PesquisarPorId(usuario.Id)).ReturnsAsync(usuario);
            ClassicAssert.IsNotNull(_usuarioService.PesquisarPorId(usuario.Id));
        }

        [Test]
        public void ObterUsuarioParaAutenticacaoNaoLocalizadoTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 06", "Teste 06", "Teste06");

            LoginDto loginDto = new LoginDto();
            loginDto.Login = "Teste";
            loginDto.Senha = "Teste";
            loginDto.Tenant = "Teste";

            _usuarioRepository.Setup(r => r.PesquisarPorLoginSenha(usuario.Login, usuario.Senha)).ReturnsAsync(usuario);

            QuironException? exception = ClassicAssert.ThrowsAsync<QuironException>(() => _usuarioService.ObterUsuarioParaAutenticacao(loginDto));

            if (exception != null)
                ClassicAssert.IsTrue(exception.Message.Equals("Usuário não localizado."));
        }

        [Test]
        public void ObterUsuarioParaAutenticacaoTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 07", "Teste 07", "Teste07");

            LoginDto loginDto = new LoginDto();
            loginDto.Login = usuario.Login;
            loginDto.Senha = usuario.Senha;

            _usuarioRepository.Setup(r => r.PesquisarPorLoginSenha(usuario.Login, usuario.Senha)).ReturnsAsync(usuario);
            ClassicAssert.IsNotNull(_usuarioService.ObterUsuarioParaAutenticacao(loginDto));
        }
    }
}