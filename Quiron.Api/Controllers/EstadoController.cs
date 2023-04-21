using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Quiron.Domain.Dto;
using Quiron.Domain.Exception;
using Quiron.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
        [ProducesResponseType(typeof(IList<EstadoDto>), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult ObterTodosPorUf(string uf)
            => Ok(_estadoService.ObterTodosPorUf(uf));

        /// <summary>
        /// Criar
        /// </summary>
        /// <param name="estado"></param>
        /// <response code="200">Criado com sucesso.</response>
        /// <response code="400">Não foi possível criar.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Post(EstadoDto estado)
        {
            if (!ModelState.IsValid)
            {
                throw new QuironException("Os dados para criação são inválidos.");
            }

            _estadoService.Criar(estado);
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
        public IActionResult Put(EstadoDto estado)
        {
            if (!ModelState.IsValid)
            {
                throw new QuironException("Os dados para atualização são inválidos.");
            }

            if ((estado.Id.ToString().Equals("")) || (_estadoService.PesquisarPorId(estado.Id) == null))
            {
                return NotFound();
            }

            _estadoService.Atualizar(estado);
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
        public IActionResult Delete(Guid id)
        {
            EstadoDto estado = _estadoService.PesquisarPorId(id);
            if (estado == null)
            {
                return NotFound();
            }

            _estadoService.Remover(estado);
            return Ok();
        }
    }
}