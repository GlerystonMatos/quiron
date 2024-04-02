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

        public void Criar(EstadoDto estado)
            => _estadoRepository.Criar(_mapper.Map<Estado>(estado));

        public async Task AtualizarAsync(EstadoDto origem)
        {
            Estado destino = await _estadoRepository.PesquisarPorIdAsync(origem.Id);
            _mapper.Map(origem, destino);
        }

        public async Task RemoverAsync(Guid id)
        {
            Estado estado = await _estadoRepository.PesquisarPorIdAsync(id);
            _estadoRepository.Remover(estado);
        }

        public Task SalvarAlteracoesAsync()
            => _estadoRepository.SalvarAlteracoesAsync();

        public async Task<EstadoDto> PesquisarPorIdAsync(Guid id)
            => _mapper.Map<EstadoDto>(await _estadoRepository.PesquisarPorIdAsync(id));

        public IQueryable<EstadoDto> ObterTodos()
            => _estadoRepository.ObterTodos().ProjectTo<EstadoDto>(_mapper.ConfigurationProvider);

        public async Task<EstadoDto[]> ObterTodosPorUfAsync(string uf)
        {
            TenantConfiguration tenant = _tenantService.Get();
            return _mapper.Map<EstadoDto[]>(await _estadoQuery.ObterTodosPorUfAsync(tenant.ConnectionStringDados, uf));
        }
    }
}