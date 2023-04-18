using NUnit.Framework;
using Quiron.Data.Repositories;
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
        public void CriarTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Peixe");
            _animalRepository.Criar(animal);

            Animal novoAnimal = _animalRepository.PesquisarPorId(animal.Id);
            Assert.IsNotNull(novoAnimal);
        }

        [Test]
        public void AtualizarTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Urso");
            _animalRepository.Criar(animal);

            animal.Nome = "Urso Pardo";
            _animalRepository.Atualizar(animal);

            Animal? animalAtualizado = _animalRepository.ObterTodos().Where(a => a.Nome.Equals(animal.Nome)).FirstOrDefault();
            Assert.IsNotNull(animalAtualizado);
        }

        [Test]
        public void RemoverTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Coelho");
            _animalRepository.Criar(animal);

            _animalRepository.Remover(animal);
            Animal animalRemovido = _animalRepository.PesquisarPorId(animal.Id);

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
        public void PesquisarPorIdTest()
        {
            Animal animal = new Animal(Guid.NewGuid(), "Tatu");
            _animalRepository.Criar(animal);

            Animal animalPesquisa = _animalRepository.PesquisarPorId(animal.Id);
            Assert.IsNotNull(animalPesquisa);
        }
    }
}