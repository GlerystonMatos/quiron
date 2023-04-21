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
    public class EstadoServiceTest
    {
        private IMapper _mapper;
        private ITenantService _tenantService;
        private IEstadoService _estadoService;
        private Mock<IEstadoQuery> _estadoQuery;
        private Mock<IEstadoRepository> _estadoRepository;

        public EstadoServiceTest()
        {
            _mapper = Mapeador.GetMapper();
            _tenantService = Tenant.GetTenant();
            _estadoQuery = new Mock<IEstadoQuery>();
            _estadoRepository = new Mock<IEstadoRepository>();
            _estadoService = new EstadoService(_mapper, _tenantService, _estadoQuery.Object, _estadoRepository.Object);
        }

        [Test]
        public void CriarTest()
        {
            EstadoDto estado = new EstadoDto();
            Assert.DoesNotThrow(() => _estadoService.Criar(estado));
        }

        [Test]
        public void AtualizarTest()
        {
            EstadoDto estado = new EstadoDto();
            Assert.DoesNotThrow(() => _estadoService.Atualizar(estado));
        }

        [Test]
        public void RemoverTest()
        {
            EstadoDto estado = new EstadoDto();
            Assert.DoesNotThrow(() => _estadoService.Remover(estado));
        }

        [Test]
        public void ObterTodosTest()
        {
            Estado estado01 = new Estado(Guid.NewGuid(), "Ceará", "CE");
            Estado estado02 = new Estado(Guid.NewGuid(), "Rio Grande do Norte", "RN");

            IList<Estado> estados = new List<Estado>();
            estados.Add(estado01);
            estados.Add(estado02);

            _estadoRepository.Setup(r => r.ObterTodos()).Returns(estados.AsQueryable());
            Assert.IsNotNull(_estadoService.ObterTodos());
        }

        [Test]
        public void PesquisarPorIdTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Ceará", "CE");

            _estadoRepository.Setup(r => r.PesquisarPorId(estado.Id)).Returns(estado);
            Assert.IsNotNull(_estadoService.PesquisarPorId(estado.Id));
        }

        [Test]
        public void ObterTodosPorUfTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Ceará", "CE");

            IList<Estado> estados = new List<Estado>();
            estados.Add(estado);

            string uf = "CE";

            TenantConfiguration tenant = _tenantService.Get();

            _estadoQuery.Setup(r => r.ObterTodosPorUf(tenant.ConnectionStringDados, uf)).Returns(estados);
            Assert.IsNotNull(_estadoService.ObterTodosPorUf(uf));
        }
    }
}