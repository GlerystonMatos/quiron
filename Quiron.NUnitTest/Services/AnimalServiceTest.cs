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
    public class AnimalServiceTest
    {
        private IMapper _mapper;
        private ITenantService _tenantService;
        private IAnimalService _animalService;
        private Mock<IAnimalQuery> _animalQuery;
        private Mock<IAnimalRepository> _animalRepository;

        public AnimalServiceTest()
        {
            _mapper = Mapeador.Get();
            _tenantService = Tenant.Get();
            _animalQuery = new Mock<IAnimalQuery>();
            _animalRepository = new Mock<IAnimalRepository>();
            _animalService = new AnimalService(_mapper, _tenantService, _animalQuery.Object, _animalRepository.Object);
        }

        [Test]
        public void CriarTest()
        {
            AnimalDto animal = new AnimalDto();
            ClassicAssert.DoesNotThrow(() => _animalService.Criar(animal));
        }

        [Test]
        public void AtualizarTest()
        {
            AnimalDto animal = new AnimalDto();
            ClassicAssert.DoesNotThrow(() => _animalService.Atualizar(animal));
        }

        [Test]
        public void RemoverTest()
            => ClassicAssert.DoesNotThrow(() => _animalService.Remover(Guid.NewGuid()));

        [Test]
        public void ObterTodosTest()
        {
            Animal animal01 = new Animal(Guid.NewGuid(), "Peixe");
            Animal animal02 = new Animal(Guid.NewGuid(), "Urso");

            IList<Animal> animais = new List<Animal>();
            animais.Add(animal01);
            animais.Add(animal02);

            _animalRepository.Setup(r => r.ObterTodos()).Returns(animais.AsQueryable());
            ClassicAssert.IsNotNull(_animalService.ObterTodos());
        }

        [Test]
        public void PesquisarPorIdTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Coelho");

            _animalRepository.Setup(r => r.PesquisarPorId(animal.Id)).ReturnsAsync(animal);
            ClassicAssert.IsNotNull(_animalService.PesquisarPorId(animal.Id));
        }

        [Test]
        public void ObterTodosPorNomeTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Babuíno");

            IList<Animal> animais = new List<Animal>();
            animais.Add(animal);

            string nome = "Babuíno";

            TenantConfiguration tenant = _tenantService.Get();

            _animalQuery.Setup(r => r.ObterTodosPorNome(tenant.ConnectionStringDados, nome)).ReturnsAsync(animais.ToArray());
            ClassicAssert.IsNotNull(_animalService.ObterTodosPorNome(nome));
        }
    }
}