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
        public async Task CriarTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Aquiraz", _idCeara);
            _cidadeRepository.Criar(cidade);

            Cidade novoCidade = await _cidadeRepository.PesquisarPorIdAsync(cidade.Id);
            Assert.That(novoCidade, Is.Not.Null);
        }

        [Test]
        public async Task AtualizarTest()
        {
            Cidade cidade = await _cidadeRepository.PesquisarPorIdAsync(Guid.Parse("373fad00-4ace-4c53-abbd-4fa11212cd88"));
            cidade.Nome = "Aracati";

            await _cidadeRepository.SalvarAlteracoesAsync();

            Cidade? atualizado = _cidadeRepository.ObterTodos().Where(a => a.Nome.Equals(cidade.Nome)).FirstOrDefault();
            Assert.That(atualizado, Is.Not.Null);

            cidade.Nome = "Fortaleza";

            await _cidadeRepository.SalvarAlteracoesAsync();
        }

        [Test]
        public async Task RemoverTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Itapipoca", _idCeara);
            _cidadeRepository.Criar(cidade);

            _cidadeRepository.Remover(cidade);
            Cidade cidadeRemovido = await _cidadeRepository.PesquisarPorIdAsync(cidade.Id);

            Assert.That(cidadeRemovido, Is.Null);
        }

        [Test]
        public void ObterTodosTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Quixadá", _idCeara);
            _cidadeRepository.Criar(cidade);

            IQueryable<Cidade> cidades = _cidadeRepository.ObterTodos();
            Assert.That(cidades, Is.Not.Null);
        }

        [Test]
        public async Task PesquisarPorIdAsyncTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Juazeiro do Norte", _idCeara);
            _cidadeRepository.Criar(cidade);

            Cidade cidadePesquisa = await _cidadeRepository.PesquisarPorIdAsync(cidade.Id);
            Assert.That(cidadePesquisa, Is.Not.Null);
        }
    }
}