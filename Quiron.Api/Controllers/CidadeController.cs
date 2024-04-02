using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Quiron.Domain.Dto;
using Quiron.Domain.Exception;
using Quiron.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadeController(ICidadeService cidadeService)
            => _cidadeService = cidadeService;

        /// <summary>
        /// Consulta
        /// </summary>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        [HttpGet]
        [EnableQuery()]
        [ProducesResponseType(typeof(IQueryable<CidadeDto>), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Get()
            => Ok(_cidadeService.ObterTodos());

        /// <summary>
        /// Consulta por nome
        /// </summary>
        /// <param name="nome"></param>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        [HttpGet("{nome}")]
        [ProducesResponseType(typeof(CidadeDto[]), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> ObterTodosPorNomeAsync(string nome)
            => Ok(await _cidadeService.ObterTodosPorNomeAsync(nome));

        /// <summary>
        /// Criar
        /// </summary>
        /// <param name="cidade"></param>
        /// <response code="200">Criado com sucesso.</response>
        /// <response code="400">Não foi possível criar.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Post(CidadeDto cidade)
        {
            if (!ModelState.IsValid)
                throw new QuironException("Os dados para criação são inválidos.");

            _cidadeService.Criar(cidade);
            await _cidadeService.SalvarAlteracoesAsync();

            return Ok();
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <param name="cidade"></param>
        /// <response code="200">Atualizado com sucesso.</response>
        /// <response code="400">Não foi possível atualizar.</response>
        /// <response code="404">Não localizado.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Put(CidadeDto cidade)
        {
            if (!ModelState.IsValid)
                throw new QuironException("Os dados para atualização são inválidos.");

            if ((cidade.Id.ToString().Equals("")) || (await _cidadeService.PesquisarPorIdAsync(cidade.Id) == null))
                return NotFound();

            await _cidadeService.AtualizarAsync(cidade);
            await _cidadeService.SalvarAlteracoesAsync();

            return Ok();
        }

        /// <summary>
        /// Excluir
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Exclusão realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a exclusão.</response>
        /// <response code="404">Não localizado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            CidadeDto cidade = await _cidadeService.PesquisarPorIdAsync(id);

            if (cidade == null)
                return NotFound();

            await _cidadeService.RemoverAsync(cidade.Id);
            await _cidadeService.SalvarAlteracoesAsync();

            return Ok();
        }
    }
}