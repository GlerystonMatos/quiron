using AutoMapper;
using Moq;
using NUnit.Framework;
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
            => Assert.DoesNotThrow(() => _usuarioService.Criar(new UsuarioDto()));

        [Test]
        public void AtualizarAsyncTest()
            => Assert.DoesNotThrowAsync(() => _usuarioService.AtualizarAsync(new UsuarioDto()));

        [Test]
        public void RemoverAsyncTest()
            => Assert.DoesNotThrowAsync(() => _usuarioService.RemoverAsync(Guid.NewGuid()));

        [Test]
        public void ObterTodosTest()
        {
            Usuario usuario01 = new Usuario(Guid.NewGuid(), "Teste 03", "Teste 03", "Teste03");
            Usuario usuario02 = new Usuario(Guid.NewGuid(), "Teste 04", "Teste 04", "Teste04");

            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuario01);
            usuarios.Add(usuario02);

            _usuarioRepository.Setup(r => r.ObterTodos()).Returns(usuarios.AsQueryable());
            Assert.That(_usuarioService.ObterTodos(), Is.Not.Null);
        }

        [Test]
        public void PesquisarPorIdAsyncTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 05", "Teste 05", "Teste05");

            _usuarioRepository.Setup(r => r.PesquisarPorIdAsync(usuario.Id)).ReturnsAsync(usuario);
            Assert.ThatAsync(() => _usuarioService.PesquisarPorIdAsync(usuario.Id), Is.Not.Null);
        }

        [Test]
        public void ObterUsuarioParaAutenticacaoAsyncNaoLocalizadoTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 06", "Teste 06", "Teste06");

            LoginDto loginDto = new LoginDto();
            loginDto.Login = "Teste";
            loginDto.Senha = "Teste";
            loginDto.Tenant = "Teste";

            _usuarioRepository.Setup(r => r.PesquisarPorLoginSenhaAsync(usuario.Login, usuario.Senha)).ReturnsAsync(usuario);

            QuironException? exception = Assert.ThrowsAsync<QuironException>(() => _usuarioService.ObterUsuarioParaAutenticacaoAsync(loginDto));

            if (exception != null)
                Assert.That(exception.Message.Equals("Usuário não localizado."), Is.True);
        }

        [Test]
        public void ObterUsuarioParaAutenticacaoAsyncTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 07", "Teste 07", "Teste07");

            LoginDto loginDto = new LoginDto();
            loginDto.Login = usuario.Login;
            loginDto.Senha = usuario.Senha;

            _usuarioRepository.Setup(r => r.PesquisarPorLoginSenhaAsync(usuario.Login, usuario.Senha)).ReturnsAsync(usuario);
            Assert.ThatAsync(() => _usuarioService.ObterUsuarioParaAutenticacaoAsync(loginDto), Is.Not.Null);
        }
    }
}