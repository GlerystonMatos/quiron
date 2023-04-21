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

        public void Criar(CidadeDto cidade)
            => _cidadeRepository.Criar(_mapper.Map<Cidade>(cidade));

        public void Atualizar(CidadeDto cidade)
            => _cidadeRepository.Atualizar(_mapper.Map<Cidade>(cidade));

        public void Remover(CidadeDto cidade)
            => _cidadeRepository.Remover(_mapper.Map<Cidade>(cidade));

        public CidadeDto PesquisarPorId(Guid id)
            => _mapper.Map<CidadeDto>(_cidadeRepository.PesquisarPorId(id));

        public IQueryable<CidadeDto> ObterTodos()
            => _cidadeRepository.ObterTodos().ProjectTo<CidadeDto>(_mapper.ConfigurationProvider);

        public IList<CidadeDto> ObterTodosPorNome(string nome)
        {
            TenantConfiguration tenant = _tenantService.Get();
            return _mapper.Map<IList<CidadeDto>>(_cidadeQuery.ObterTodosPorNome(tenant.ConnectionStringDados, nome));
        }
    }
}