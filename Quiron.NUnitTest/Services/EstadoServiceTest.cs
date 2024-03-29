﻿using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
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
        {
            EstadoDto estado = new EstadoDto();
            ClassicAssert.DoesNotThrow(() => _estadoService.Criar(estado));
        }

        [Test]
        public void AtualizarTest()
        {
            EstadoDto estado = new EstadoDto();
            ClassicAssert.DoesNotThrow(() => _estadoService.Atualizar(estado));
        }

        [Test]
        public void RemoverTest()
            => ClassicAssert.DoesNotThrow(() => _estadoService.Remover(Guid.NewGuid()));

        [Test]
        public void ObterTodosTest()
        {
            Estado estado01 = new Estado(Guid.NewGuid(), "Ceará", "CE");
            Estado estado02 = new Estado(Guid.NewGuid(), "Rio Grande do Norte", "RN");

            IList<Estado> estados = new List<Estado>();
            estados.Add(estado01);
            estados.Add(estado02);

            _estadoRepository.Setup(r => r.ObterTodos()).Returns(estados.AsQueryable());
            ClassicAssert.IsNotNull(_estadoService.ObterTodos());
        }

        [Test]
        public void PesquisarPorIdTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Ceará", "CE");

            _estadoRepository.Setup(r => r.PesquisarPorId(estado.Id)).ReturnsAsync(estado);
            ClassicAssert.IsNotNull(_estadoService.PesquisarPorId(estado.Id));
        }

        [Test]
        public void ObterTodosPorUfTest()
        {
            Estado estado = new Estado(Guid.NewGuid(), "Ceará", "CE");

            IList<Estado> estados = new List<Estado>();
            estados.Add(estado);

            string uf = "CE";

            TenantConfiguration tenant = _tenantService.Get();

            _estadoQuery.Setup(r => r.ObterTodosPorUf(tenant.ConnectionStringDados, uf)).ReturnsAsync(estados.ToArray());
            ClassicAssert.IsNotNull(_estadoService.ObterTodosPorUf(uf));
        }
    }
}