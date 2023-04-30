using NUnit.Framework;
using Quiron.Data.EF.Repositories;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;
using Quiron.NUnitTest.Utilitarios;

namespace Quiron.NUnitTest.Repositories
{
    public class AnimalRepositoryTest
    {
        private IAnimalRepository _animalRepository;

        public AnimalRepositoryTest()
            => _animalRepository = new AnimalRepository(Conexao.GetContext());

        [Test]
        public async Task CriarTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Peixe");
            _animalRepository.Criar(animal);

            Animal novoAnimal = await _animalRepository.PesquisarPorId(animal.Id);
            Assert.IsNotNull(novoAnimal);
        }

        [Test]
        public async Task AtualizarTest()
        {
            Animal animal = await _animalRepository.PesquisarPorId(Guid.Parse("1dfc4a8d-7ed1-443c-9cc7-ac71ea9d003b"));
            animal.Nome = "Urso";

            await _animalRepository.SalvarAlteracoes();

            Animal? atualizado = _animalRepository.ObterTodos().Where(a => a.Nome.Equals(animal.Nome)).FirstOrDefault();
            Assert.IsNotNull(atualizado);

            animal.Nome = "Cachorro";

            await _animalRepository.SalvarAlteracoes();
        }

        [Test]
        public async Task RemoverTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Coelho");
            _animalRepository.Criar(animal);

            _animalRepository.Remover(animal);
            Animal animalRemovido = await _animalRepository.PesquisarPorId(animal.Id);

            Assert.IsNull(animalRemovido);
        }

        [Test]
        public void ObterTodosTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Lobo");
            _animalRepository.Criar(animal);

            IQueryable<Animal> animals = _animalRepository.ObterTodos();
            Assert.IsNotNull(animals);
        }

        [Test]
        public async Task PesquisarPorIdTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Tatu");
            _animalRepository.Criar(animal);

            Animal animalPesquisa = await _animalRepository.PesquisarPorId(animal.Id);
            Assert.IsNotNull(animalPesquisa);
        }
    }
}