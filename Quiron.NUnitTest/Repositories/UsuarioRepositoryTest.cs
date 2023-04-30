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

            Usuario novoUsuario = await _usuarioRepository.PesquisarPorId(usuario.Id);
            Assert.IsNotNull(novoUsuario);
        }

        [Test]
        public async Task AtualizarTest()
        {
            Usuario usuario = await _usuarioRepository.PesquisarPorId(Guid.Parse("d78a657f-66fa-43f2-a535-212e6bfb6630"));
            usuario.Nome = "Teste 04";

            await _usuarioRepository.SalvarAlteracoes();

            Usuario? atualizado = _usuarioRepository.ObterTodos().Where(a => a.Nome.Equals(usuario.Nome)).FirstOrDefault();
            Assert.IsNotNull(atualizado);

            usuario.Nome = "Teste 01";

            await _usuarioRepository.SalvarAlteracoes();
        }

        [Test]
        public async Task RemoverTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 05", "Teste 05", "Teste05");
            _usuarioRepository.Criar(usuario);

            _usuarioRepository.Remover(usuario);
            Usuario usuarioRemovido = await _usuarioRepository.PesquisarPorId(usuario.Id);

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
        public async Task PesquisarPorIdTest()
        {
            Usuario usuario = new Usuario(Guid.NewGuid(), "Teste 07", "Teste 07", "Teste07");
            _usuarioRepository.Criar(usuario);

            Usuario usuarioPesquisa = await _usuarioRepository.PesquisarPorId(usuario.Id);
            Assert.IsNotNull(usuarioPesquisa);
        }

        [Test]
        public async Task PesquisarPorLoginSenhaTest()
        {
            Usuario usuarioPesquisa = await _usuarioRepository.PesquisarPorLoginSenha("Teste01", "1234");
            Assert.IsNotNull(usuarioPesquisa);
        }
    }
}