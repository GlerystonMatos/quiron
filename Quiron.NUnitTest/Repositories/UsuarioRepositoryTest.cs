using NUnit.Framework;
using Quiron.Data.Repositories;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;
using Quiron.NUnitTest.Utilitarios;

namespace Quiron.NUnitTest.Repositories
{
    public class UsuarioRepositoryTest
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioRepositoryTest()
            => _usuarioRepository = new UsuarioRepository(Conexao.GetContext());

        [Test]
        public void CriarTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 03", "Teste 03", "Teste03");
            _usuarioRepository.Criar(usuario);

            Usuario novoUsuario = _usuarioRepository.PesquisarPorId(usuario.Id);
            Assert.IsNotNull(novoUsuario);
        }

        [Test]
        public void AtualizarTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 04", "Teste 04", "Teste04");
            _usuarioRepository.Criar(usuario);

            usuario.Nome = "Teste 04.01";
            _usuarioRepository.Atualizar(usuario);

            Usuario? usuarioAtualizado = _usuarioRepository.ObterTodos().Where(a => a.Nome.Equals(usuario.Nome)).FirstOrDefault();
            Assert.IsNotNull(usuarioAtualizado);
        }

        [Test]
        public void RemoverTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 05", "Teste 05", "Teste05");
            _usuarioRepository.Criar(usuario);

            _usuarioRepository.Remover(usuario);
            Usuario usuarioRemovido = _usuarioRepository.PesquisarPorId(usuario.Id);

            Assert.IsNull(usuarioRemovido);
        }

        [Test]
        public void ObterTodosTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 06", "Teste 06", "Teste06");
            _usuarioRepository.Criar(usuario);

            IQueryable<Usuario> usuarios = _usuarioRepository.ObterTodos();
            Assert.IsNotNull(usuarios);
        }

        [Test]
        public void PesquisarPorIdTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 07", "Teste 07", "Teste07");
            _usuarioRepository.Criar(usuario);

            Usuario usuarioPesquisa = _usuarioRepository.PesquisarPorId(usuario.Id);
            Assert.IsNotNull(usuarioPesquisa);
        }
    }
}