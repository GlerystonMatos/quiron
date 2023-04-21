using NUnit.Framework;
using Quiron.Data.EF.Repositories;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;
using Quiron.NUnitTest.Utilitarios;

namespace Quiron.NUnitTest.Repositories
{
    public class CidadeRepositoryTest
    {
        private Guid _idCeara;
        private ICidadeRepository _cidadeRepository;

        public CidadeRepositoryTest()
        {
            _idCeara = Guid.Parse("362c52b3-b9db-4aca-a48f-6e47aa77f819");
            _cidadeRepository = new CidadeRepository(Conexao.GetContext());
        }

        [Test]
        public void CriarTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Aquiraz", _idCeara);
            _cidadeRepository.Criar(cidade);

            Cidade novoCidade = _cidadeRepository.PesquisarPorId(cidade.Id);
            Assert.IsNotNull(novoCidade);
        }

        [Test]
        public void AtualizarTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Aracati", _idCeara);
            _cidadeRepository.Criar(cidade);

            cidade.Nome = "Aracati - CE";
            _cidadeRepository.Atualizar(cidade);

            Cidade? cidadeAtualizado = _cidadeRepository.ObterTodos().Where(a => a.Nome.Equals(cidade.Nome)).FirstOrDefault();
            Assert.IsNotNull(cidadeAtualizado);
        }

        [Test]
        public void RemoverTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Itapipoca", _idCeara);
            _cidadeRepository.Criar(cidade);

            _cidadeRepository.Remover(cidade);
            Cidade cidadeRemovido = _cidadeRepository.PesquisarPorId(cidade.Id);

            Assert.IsNull(cidadeRemovido);
        }

        [Test]
        public void ObterTodosTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Quixadá", _idCeara);
            _cidadeRepository.Criar(cidade);

            IQueryable<Cidade> cidades = _cidadeRepository.ObterTodos();
            Assert.IsNotNull(cidades);
        }

        [Test]
        public void PesquisarPorIdTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Juazeiro do Norte", _idCeara);
            _cidadeRepository.Criar(cidade);

            Cidade cidadePesquisa = _cidadeRepository.PesquisarPorId(cidade.Id);
            Assert.IsNotNull(cidadePesquisa);
        }
    }
}