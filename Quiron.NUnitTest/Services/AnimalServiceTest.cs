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
            => Assert.DoesNotThrow(() => _animalService.Criar(new AnimalDto()));

        [Test]
        public void AtualizarAsyncTest()
            => Assert.DoesNotThrowAsync(() => _animalService.AtualizarAsync(new AnimalDto()));

        [Test]
        public void RemoverAsyncTest()
            => Assert.DoesNotThrow(() => _animalService.RemoverAsync(Guid.NewGuid()));

        [Test]
        public void ObterTodosTest()
        {
            Animal animal01 = new Animal(Guid.NewGuid(), "Peixe");
            Animal animal02 = new Animal(Guid.NewGuid(), "Urso");

            IList<Animal> animais = new List<Animal>();
            animais.Add(animal01);
            animais.Add(animal02);

            _animalRepository.Setup(r => r.ObterTodos()).Returns(animais.AsQueryable());
            Assert.That(_animalService.ObterTodos(), Is.Not.Null);
        }

        [Test]
        public void PesquisarPorIdAsyncTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Coelho");

            _animalRepository.Setup(r => r.PesquisarPorIdAsync(animal.Id)).ReturnsAsync(animal);
            Assert.ThatAsync(() => _animalService.PesquisarPorIdAsync(animal.Id), Is.Not.Null);
        }

        [Test]
        public void ObterTodosPorNomeAsyncTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Babuíno");

            IList<Animal> animais = new List<Animal>();
            animais.Add(animal);

            string nome = "Babuíno";

            TenantConfiguration tenant = _tenantService.Get();

            _animalQuery.Setup(r => r.ObterTodosPorNomeAsync(tenant.ConnectionStringDados, nome)).ReturnsAsync(animais.ToArray());
            Assert.ThatAsync(() => _animalService.ObterTodosPorNomeAsync(nome), Is.Not.Null);
        }
    }
}