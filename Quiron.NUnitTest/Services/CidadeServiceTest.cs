using AutoMapper;
using Moq;
using NUnit.Framework;
using Quiron.Domain.Dto;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;
using Quiron.Domain.Interfaces.Queries;
using Quiron.Domain.Interfaces.Services;
using Quiron.Domain.Tenant;
using Quiron.NUnitTest.Utilitarios;
using Quiron.Service.Services;

namespace Quiron.NUnitTest.Services
{
    public class CidadeServiceTest
    {
        private IMapper _mapper;
        private ITenantService _tenantService;
        private ICidadeService _cidadeService;
        private Mock<ICidadeQuery> _cidadeQuery;
        private Mock<ICidadeRepository> _cidadeRepository;

        public CidadeServiceTest()
        {
            _mapper = Mapeador.Get();
            _tenantService = Tenant.Get();
            _cidadeQuery = new Mock<ICidadeQuery>();
            _cidadeRepository = new Mock<ICidadeRepository>();
            _cidadeService = new CidadeService(_mapper, _tenantService, _cidadeQuery.Object, _cidadeRepository.Object);
        }

        [Test]
        public void CriarTest()
            => Assert.DoesNotThrow(() => _cidadeService.Criar(new CidadeDto()));

        [Test]
        public void AtualizarAsyncTest()
            => Assert.DoesNotThrowAsync(() => _cidadeService.AtualizarAsync(new CidadeDto()));

        [Test]
        public void RemoverAsyncTest()
            => Assert.DoesNotThrowAsync(() => _cidadeService.RemoverAsync(Guid.NewGuid()));

        [Test]
        public void ObterTodosTest()
        {
            Cidade cidade01 = new Cidade(Guid.NewGuid(), "Fortaleza", Guid.NewGuid());
            Cidade cidade02 = new Cidade(Guid.NewGuid(), "Caucaia", Guid.NewGuid());

            IList<Cidade> cidades = new List<Cidade>();
            cidades.Add(cidade01);
            cidades.Add(cidade02);

            _cidadeRepository.Setup(r => r.ObterTodos()).Returns(cidades.AsQueryable());
            Assert.That(_cidadeService.ObterTodos(), Is.Not.Null);
        }

        [Test]
        public void PesquisarPorIdAsyncTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Fortaleza", Guid.NewGuid());

            _cidadeRepository.Setup(r => r.PesquisarPorIdAsync(cidade.Id)).ReturnsAsync(cidade);
            Assert.ThatAsync(() => _cidadeService.PesquisarPorIdAsync(cidade.Id), Is.Not.Null);
        }

        [Test]
        public void ObterTodosPorNomeAsyncTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Fortaleza", Guid.NewGuid());

            IList<Cidade> cidades = new List<Cidade>();
            cidades.Add(cidade);

            TenantConfiguration tenant = _tenantService.Get();

            _cidadeQuery.Setup(r => r.ObterTodosPorNomeAsync(tenant.ConnectionStringDados, cidade.Nome)).ReturnsAsync(cidades.ToArray());
            Assert.ThatAsync(() => _cidadeService.ObterTodosPorNomeAsync(cidade.Nome), Is.Not.Null);
        }
    }
}