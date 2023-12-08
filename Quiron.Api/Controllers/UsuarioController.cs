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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
            => _usuarioService = usuarioService;

        /// <summary>
        /// Consulta
        /// </summary>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        [HttpGet]
        [EnableQuery()]
        [ProducesResponseType(typeof(IQueryable<UsuarioDto>), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Get()
            => Ok(_usuarioService.ObterTodos());

        /// <summary>
        /// Criar
        /// </summary>
        /// <param name="usuario"></param>
        /// <response code="200">Criado com sucesso.</response>
        /// <response code="400">Não foi possível criar.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Post(UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
                throw new QuironException("Os dados para criação são inválidos.");

            _usuarioService.Criar(usuario);
            await _usuarioService.SalvarAlteracoes();

            return Ok();
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <param name="usuario"></param>
        /// <response code="200">Atualizado com sucesso.</response>
        /// <response code="400">Não foi possível atualizar.</response>
        /// <response code="404">Não localizado.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Put(UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
                throw new QuironException("Os dados para atualização são inválidos.");

            if ((usuario.Id.ToString().Equals("")) || (await _usuarioService.PesquisarPorId(usuario.Id) == null))
                return NotFound();

            _usuarioService.Atualizar(usuario);
            await _usuarioService.SalvarAlteracoes();

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
            UsuarioDto usuario = await _usuarioService.PesquisarPorId(id);
            if (usuario == null)
                return NotFound();

            _usuarioService.Remover(usuario.Id);
            await _usuarioService.SalvarAlteracoes();

            return Ok();
        }
    }
}