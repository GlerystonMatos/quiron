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

        public async Task Criar(EstadoDto estado)
        {
            _estadoRepository.Criar(_mapper.Map<Estado>(estado));
            await _estadoRepository.SalvarAlteracoes();
        }

        public async Task Atualizar(EstadoDto origem)
        {
            Estado destino = await _estadoRepository.PesquisarPorId(origem.Id);
            _mapper.Map(origem, destino);

            await _estadoRepository.SalvarAlteracoes();
        }

        public async Task Remover(Guid id)
        {
            Estado estado = await _estadoRepository.PesquisarPorId(id);
            _estadoRepository.Remover(estado);

            await _estadoRepository.SalvarAlteracoes();
        }

        public async Task<EstadoDto> PesquisarPorId(Guid id)
            => _mapper.Map<EstadoDto>(await _estadoRepository.PesquisarPorId(id));

        public IQueryable<EstadoDto> ObterTodos()
            => _estadoRepository.ObterTodos().ProjectTo<EstadoDto>(_mapper.ConfigurationProvider);

        public async Task<EstadoDto[]> ObterTodosPorUf(string uf)
        {
            TenantConfiguration tenant = _tenantService.Get();
            return _mapper.Map<EstadoDto[]>(await _estadoQuery.ObterTodosPorUf(tenant.ConnectionStringDados, uf));
        }
    }
}