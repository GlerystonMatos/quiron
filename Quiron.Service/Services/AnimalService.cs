using AutoMapper;
using AutoMapper.QueryableExtensions;
using Quiron.Domain.Dto;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;
using Quiron.Domain.Interfaces.Queries;
using Quiron.Domain.Interfaces.Services;
using Quiron.Domain.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Criar(AnimalDto animal)
            => _animalRepository.Criar(_mapper.Map<Animal>(animal));

        public void Atualizar(AnimalDto animal)
            => _animalRepository.Atualizar(_mapper.Map<Animal>(animal));

        public void Remover(AnimalDto animal)
            => _animalRepository.Remover(_mapper.Map<Animal>(animal));

        public AnimalDto PesquisarPorId(Guid id)
            => _mapper.Map<AnimalDto>(_animalRepository.PesquisarPorId(id));

        public IQueryable<AnimalDto> ObterTodos()
            => _animalRepository.ObterTodos().ProjectTo<AnimalDto>(_mapper.ConfigurationProvider);

        public IList<AnimalDto> ObterTodosPorNome(string nome)
        {
            TenantConfiguration tenant = _tenantService.Get();
            return _mapper.Map<IList<AnimalDto>>(_animalQuery.ObterTodosPorNome(tenant.ConnectionStringDados, nome));
        }
    }
}