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
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _estadoService;

        public EstadoController(IEstadoService estadoService)
            => _estadoService = estadoService;

        /// <summary>
        /// Consulta
        /// </summary>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        [HttpGet]
        [EnableQuery()]
        [ProducesResponseType(typeof(IQueryable<EstadoDto>), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Get()
            => Ok(_estadoService.ObterTodos());

        /// <summary>
        /// Consulta por uf
        /// </summary>
        /// <param name="uf"></param>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        [HttpGet("{uf}")]
        [ProducesResponseType(typeof(EstadoDto[]), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> ObterTodosPorUfAsync(string uf)
            => Ok(await _estadoService.ObterTodosPorUfAsync(uf));

        /// <summary>
        /// Criar
        /// </summary>
        /// <param name="estado"></param>
        /// <response code="200">Criado com sucesso.</response>
        /// <response code="400">Não foi possível criar.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Post(EstadoDto estado)
        {
            if (!ModelState.IsValid)
                throw new QuironException("Os dados para criação são inválidos.");

            _estadoService.Criar(estado);
            await _estadoService.SalvarAlteracoesAsync();

            return Ok();
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <param name="estado"></param>
        /// <response code="200">Atualizado com sucesso.</response>
        /// <response code="400">Não foi possível atualizar.</response>
        /// <response code="404">Não localizado.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Put(EstadoDto estado)
        {
            if (!ModelState.IsValid)
                throw new QuironException("Os dados para atualização são inválidos.");

            if ((estado.Id.ToString().Equals("")) || (await _estadoService.PesquisarPorIdAsync(estado.Id) == null))
                return NotFound();

            await _estadoService.AtualizarAsync(estado);
            await _estadoService.SalvarAlteracoesAsync();

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
            EstadoDto estado = await _estadoService.PesquisarPorIdAsync(id);

            if (estado == null)
                return NotFound();

            await _estadoService.RemoverAsync(estado.Id);
            await _estadoService.SalvarAlteracoesAsync();

            return Ok();
        }
    }
}