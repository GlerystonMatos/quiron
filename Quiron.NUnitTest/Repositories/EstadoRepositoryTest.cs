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
        public void CriarTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Bahia", "BA");
            _estadoRepository.Criar(estado);

            Estado novoEstado = _estadoRepository.PesquisarPorId(estado.Id);
            Assert.IsNotNull(novoEstado);
        }

        [Test]
        public void AtualizarTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Acre", "AC");
            _estadoRepository.Criar(estado);

            estado.Nome = "Acre - AC";
            _estadoRepository.Atualizar(estado);

            Estado? estadoAtualizado = _estadoRepository.ObterTodos().Where(a => a.Nome.Equals(estado.Nome)).FirstOrDefault();
            Assert.IsNotNull(estadoAtualizado);
        }

        [Test]
        public void RemoverTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Maranhão", "MA");
            _estadoRepository.Criar(estado);

            _estadoRepository.Remover(estado);
            Estado estadoRemovido = _estadoRepository.PesquisarPorId(estado.Id);

            Assert.IsNull(estadoRemovido);
        }

        [Test]
        public void ObterTodosTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Amazonas", "AM");
            _estadoRepository.Criar(estado);

            IQueryable<Estado> estados = _estadoRepository.ObterTodos();
            Assert.IsNotNull(estados);
        }

        [Test]
        public void PesquisarPorIdTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Mato Grosso", "MT");
            _estadoRepository.Criar(estado);

            Estado estadoPesquisa = _estadoRepository.PesquisarPorId(estado.Id);
            Assert.IsNotNull(estadoPesquisa);
        }
    }
}