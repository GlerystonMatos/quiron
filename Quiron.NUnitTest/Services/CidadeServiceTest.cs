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
            _tenantService = Tenant.Get();
            _mapper = Mapeador.GetMapper();
            _cidadeQuery = new Mock<ICidadeQuery>();
            _cidadeRepository = new Mock<ICidadeRepository>();
            _cidadeService = new CidadeService(_mapper, _tenantService, _cidadeQuery.Object, _cidadeRepository.Object);
        }

        [Test]
        public void CriarTest()
        {
            CidadeDto cidade = new CidadeDto();
            Assert.DoesNotThrow(() => _cidadeService.Criar(cidade));
        }

        [Test]
        public void AtualizarTest()
        {
            CidadeDto cidade = new CidadeDto();
            Assert.DoesNotThrow(() => _cidadeService.Atualizar(cidade));
        }

        [Test]
        public void RemoverTest()
            => Assert.DoesNotThrow(() => _cidadeService.Remover(Guid.NewGuid()));

        [Test]
        public void ObterTodosTest()
        {
            Cidade cidade01 = new Cidade(Guid.NewGuid(), "Fortaleza", Guid.NewGuid());
            Cidade cidade02 = new Cidade(Guid.NewGuid(), "Caucaia", Guid.NewGuid());

            IList<Cidade> cidades = new List<Cidade>();
            cidades.Add(cidade01);
            cidades.Add(cidade02);

            _cidadeRepository.Setup(r => r.ObterTodos()).Returns(cidades.AsQueryable());
            Assert.IsNotNull(_cidadeService.ObterTodos());
        }

        [Test]
        public void PesquisarPorIdTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Fortaleza", Guid.NewGuid());

            _cidadeRepository.Setup(r => r.PesquisarPorId(cidade.Id)).ReturnsAsync(cidade);
            Assert.IsNotNull(_cidadeService.PesquisarPorId(cidade.Id));
        }

        [Test]
        public void ObterTodosPorNomeTest()
        {
            Cidade cidade = new Cidade(Guid.NewGuid(), "Fortaleza", Guid.NewGuid());

            IList<Cidade> cidades = new List<Cidade>();
            cidades.Add(cidade);

            TenantConfiguration tenant = _tenantService.Get();

            _cidadeQuery.Setup(r => r.ObterTodosPorNome(tenant.ConnectionStringDados, cidade.Nome)).ReturnsAsync(cidades.ToArray());
            Assert.IsNotNull(_cidadeService.ObterTodosPorNome(cidade.Nome));
        }
    }
}