using NUnit.Framework;
using Quiron.Data.EF.Repositories;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;
using Quiron.NUnitTest.Utilitarios;

namespace Quiron.NUnitTest.Repositories
{
    public class EstadoRepositoryTest
    {
        private IEstadoRepository _estadoRepository;

        public EstadoRepositoryTest()
            => _estadoRepository = new EstadoRepository(Conexao.GetContext());

        [Test]
        public async Task CriarTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Bahia", "BA");
            _estadoRepository.Criar(estado);

            Estado novoEstado = await _estadoRepository.PesquisarPorIdAsync(estado.Id);
            Assert.That(novoEstado, Is.Not.Null);
        }

        [Test]
        public async Task AtualizarTest()
        {
            Estado estado = await _estadoRepository.PesquisarPorIdAsync(Guid.Parse("362c52b3-b9db-4aca-a48f-6e47aa77f819"));
            estado.Nome = "Acre";
            estado.Uf = "AC";

            await _estadoRepository.SalvarAlteracoesAsync();

            Estado? atualizado = _estadoRepository.ObterTodos().Where(e => e.Nome.Equals(estado.Nome) && e.Uf.Equals(estado.Uf)).FirstOrDefault();
            Assert.That(atualizado, Is.Not.Null);

            estado.Nome = "Ceará";
            estado.Uf = "CE";

            await _estadoRepository.SalvarAlteracoesAsync();
        }

        [Test]
        public async Task RemoverTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Maranhão", "MA");
            _estadoRepository.Criar(estado);

            _estadoRepository.Remover(estado);
            Estado estadoRemovido = await _estadoRepository.PesquisarPorIdAsync(estado.Id);

            Assert.That(estadoRemovido, Is.Null);
        }

        [Test]
        public void ObterTodosTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Amazonas", "AM");
            _estadoRepository.Criar(estado);

            IQueryable<Estado> estados = _estadoRepository.ObterTodos();
            Assert.That(estados, Is.Not.Null);
        }

        [Test]
        public async Task PesquisarPorIdAsyncTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Mato Grosso", "MT");
            _estadoRepository.Criar(estado);

            Estado estadoPesquisa = await _estadoRepository.PesquisarPorIdAsync(estado.Id);
            Assert.That(estadoPesquisa, Is.Not.Null);
        }
    }
}