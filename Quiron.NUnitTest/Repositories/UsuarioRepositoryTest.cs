using NUnit.Framework;
using Quiron.Data.EF.Repositories;
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
        public async Task CriarTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 03", "Teste 03", "Teste03");
            _usuarioRepository.Criar(usuario);

            Usuario novoUsuario = await _usuarioRepository.PesquisarPorIdAsync(usuario.Id);
            Assert.That(novoUsuario, Is.Not.Null);
        }

        [Test]
        public async Task AtualizarTest()
        {
            Usuario usuario = await _usuarioRepository.PesquisarPorIdAsync(Guid.Parse("d78a657f-66fa-43f2-a535-212e6bfb6630"));
            usuario.Nome = "Teste 04";

            await _usuarioRepository.SalvarAlteracoesAsync();

            Usuario? atualizado = _usuarioRepository.ObterTodos().Where(a => a.Nome.Equals(usuario.Nome)).FirstOrDefault();
            Assert.That(atualizado, Is.Not.Null);

            usuario.Nome = "Teste 01";

            await _usuarioRepository.SalvarAlteracoesAsync();
        }

        [Test]
        public async Task RemoverTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 05", "Teste 05", "Teste05");
            _usuarioRepository.Criar(usuario);

            _usuarioRepository.Remover(usuario);
            Usuario usuarioRemovido = await _usuarioRepository.PesquisarPorIdAsync(usuario.Id);

            Assert.That(usuarioRemovido, Is.Null);
        }

        [Test]
        public void ObterTodosTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 06", "Teste 06", "Teste06");
            _usuarioRepository.Criar(usuario);

            IQueryable<Usuario> usuarios = _usuarioRepository.ObterTodos();
            Assert.That(usuarios, Is.Not.Null);
        }

        [Test]
        public async Task PesquisarPorIdAsyncTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 07", "Teste 07", "Teste07");
            _usuarioRepository.Criar(usuario);

            Usuario usuarioPesquisa = await _usuarioRepository.PesquisarPorIdAsync(usuario.Id);
            Assert.That(usuarioPesquisa, Is.Not.Null);
        }

        [Test]
        public async Task PesquisarPorLoginSenhaAsyncTest()
        {
            Usuario usuarioPesquisa = await _usuarioRepository.PesquisarPorLoginSenhaAsync("Teste01", "1234");
            Assert.That(usuarioPesquisa, Is.Not.Null);
        }
    }
}