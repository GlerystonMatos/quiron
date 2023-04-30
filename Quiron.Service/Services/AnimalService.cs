using AutoMapper;
using AutoMapper.QueryableExtensions;
using Quiron.Domain.Dto;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;
using Quiron.Domain.Interfaces.Queries;
using Quiron.Domain.Interfaces.Services;
using Quiron.Domain.Tenant;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Service.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IMapper _mapper;
        private readonly IAnimalQuery _animalQuery;
        private readonly ITenantService _tenantService;
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IMapper mapper, ITenantService tenantService, IAnimalQuery animalQuery, IAnimalRepository animalRepository)
        {
            _mapper = mapper;
            _animalQuery = animalQuery;
            _tenantService = tenantService;
            _animalRepository = animalRepository;
        }

        public async Task Criar(AnimalDto animal)
        {
            _animalRepository.Criar(_mapper.Map<Animal>(animal));
            await _animalRepository.SalvarAlteracoes();
        }

        public async Task Atualizar(AnimalDto origem)
        {
            Animal destino = await _animalRepository.PesquisarPorId(origem.Id);
            _mapper.Map(origem, destino);

            await _animalRepository.SalvarAlteracoes();
        }

        public async Task Remover(Guid id)
        {
            Animal animal = await _animalRepository.PesquisarPorId(id);
            _animalRepository.Remover(animal);

            await _animalRepository.SalvarAlteracoes();
        }

        public async Task<AnimalDto> PesquisarPorId(Guid id)
            => _mapper.Map<AnimalDto>(await _animalRepository.PesquisarPorId(id));

        public IQueryable<AnimalDto> ObterTodos()
            => _animalRepository.ObterTodos().ProjectTo<AnimalDto>(_mapper.ConfigurationProvider);

        public async Task<AnimalDto[]> ObterTodosPorNome(string nome)
        {
            TenantConfiguration tenant = _tenantService.Get();
            return _mapper.Map<AnimalDto[]>(await _animalQuery.ObterTodosPorNome(tenant.ConnectionStringDados, nome));
        }
    }
}