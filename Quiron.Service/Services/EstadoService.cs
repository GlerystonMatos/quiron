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
    public class EstadoService : IEstadoService
    {
        private readonly IMapper _mapper;
        private readonly IEstadoQuery _estadoQuery;
        private readonly ITenantService _tenantService;
        private readonly IEstadoRepository _estadoRepository;

        public EstadoService(IMapper mapper, ITenantService tenantService, IEstadoQuery estadoQuery, IEstadoRepository estadoRepository)
        {
            _mapper = mapper;
            _estadoQuery = estadoQuery;
            _tenantService = tenantService;
            _estadoRepository = estadoRepository;
        }

        public void Criar(EstadoDto estado)
            => _estadoRepository.Criar(_mapper.Map<Estado>(estado));

        public void Atualizar(EstadoDto estado)
            => _estadoRepository.Atualizar(_mapper.Map<Estado>(estado));

        public void Remover(EstadoDto estado)
            => _estadoRepository.Remover(_mapper.Map<Estado>(estado));

        public EstadoDto PesquisarPorId(Guid id)
            => _mapper.Map<EstadoDto>(_estadoRepository.PesquisarPorId(id));

        public IQueryable<EstadoDto> ObterTodos()
            => _estadoRepository.ObterTodos().ProjectTo<EstadoDto>(_mapper.ConfigurationProvider);

        public IList<EstadoDto> ObterTodosPorUf(string uf)
        {
            TenantConfiguration tenant = _tenantService.Get();
            return _mapper.Map<IList<EstadoDto>>(_estadoQuery.ObterTodosPorUf(tenant.ConnectionStringDados, uf));
        }
    }
}