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
    public class CidadeService : ICidadeService
    {
        private readonly IMapper _mapper;
        private readonly ICidadeQuery _cidadeQuery;
        private readonly ITenantService _tenantService;
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(IMapper mapper, ITenantService tenantService, ICidadeQuery cidadeQuery, ICidadeRepository cidadeRepository)
        {
            _mapper = mapper;
            _cidadeQuery = cidadeQuery;
            _tenantService = tenantService;
            _cidadeRepository = cidadeRepository;
        }

        public async Task Criar(CidadeDto cidade)
        {
            _cidadeRepository.Criar(_mapper.Map<Cidade>(cidade));
            await _cidadeRepository.SalvarAlteracoes();
        }

        public async Task Atualizar(CidadeDto origem)
        {
            Cidade destino = await _cidadeRepository.PesquisarPorId(origem.Id);
            _mapper.Map(origem, destino);

            await _cidadeRepository.SalvarAlteracoes();
        }

        public async Task Remover(Guid id)
        {
            Cidade cidade = await _cidadeRepository.PesquisarPorId(id);
            _cidadeRepository.Remover(cidade);

            await _cidadeRepository.SalvarAlteracoes();
        }

        public async Task<CidadeDto> PesquisarPorId(Guid id)
            => _mapper.Map<CidadeDto>(await _cidadeRepository.PesquisarPorId(id));

        public IQueryable<CidadeDto> ObterTodos()
            => _cidadeRepository.ObterTodos().ProjectTo<CidadeDto>(_mapper.ConfigurationProvider);

        public async Task<CidadeDto[]> ObterTodosPorNome(string nome)
        {
            TenantConfiguration tenant = _tenantService.Get();
            return _mapper.Map<CidadeDto[]>(await _cidadeQuery.ObterTodosPorNome(tenant.ConnectionStringDados, nome));
        }
    }
}