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
            _mapper = Mapeador.Get();
            _tenantService = Tenant.Get();
            _estadoQuery = new Mock<IEstadoQuery>();
            _estadoRepository = new Mock<IEstadoRepository>();
            _estadoService = new EstadoService(_mapper, _tenantService, _estadoQuery.Object, _estadoRepository.Object);
        }

        [Test]
        public void CriarTest()
            => Assert.DoesNotThrow(() => _estadoService.Criar(new EstadoDto()));

        [Test]
        public void AtualizarAsyncTest()
            => Assert.DoesNotThrow(() => _estadoService.AtualizarAsync(new EstadoDto()));

        [Test]
        public void RemoverAsyncTest()
            => Assert.DoesNotThrow(() => _estadoService.RemoverAsync(Guid.NewGuid()));

        [Test]
        public void ObterTodosTest()
        {
            Estado estado01 = new Estado(Guid.NewGuid(), "Ceará", "CE");
            Estado estado02 = new Estado(Guid.NewGuid(), "Rio Grande do Norte", "RN");

            IList<Estado> estados = new List<Estado>();
            estados.Add(estado01);
            estados.Add(estado02);

            _estadoRepository.Setup(r => r.ObterTodos()).Returns(estados.AsQueryable());
            Assert.That(_estadoService.ObterTodos(), Is.Not.Null);
        }

        [Test]
        public void PesquisarPorIdAsyncTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Ceará", "CE");

            _estadoRepository.Setup(r => r.PesquisarPorIdAsync(estado.Id)).ReturnsAsync(estado);
            Assert.ThatAsync(() => _estadoService.PesquisarPorIdAsync(estado.Id), Is.Not.Null);
        }

        [Test]
        public void ObterTodosPorUfAsyncTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Ceará", "CE");

            IList<Estado> estados = new List<Estado>();
            estados.Add(estado);

            string uf = "CE";

            TenantConfiguration tenant = _tenantService.Get();

            _estadoQuery.Setup(r => r.ObterTodosPorUfAsync(tenant.ConnectionStringDados, uf)).ReturnsAsync(estados.ToArray());
            Assert.ThatAsync(() => _estadoService.ObterTodosPorUfAsync(uf), Is.Not.Null);
        }
    }
}