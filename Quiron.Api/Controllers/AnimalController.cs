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
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
            => _animalService = animalService;

        /// <summary>
        /// Consulta
        /// </summary>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        [HttpGet]
        [EnableQuery()]
        [ProducesResponseType(typeof(IQueryable<AnimalDto>), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Get()
            => Ok(_animalService.ObterTodos());

        /// <summary>
        /// Consulta por nome
        /// </summary>
        /// <param name="nome"></param>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        [HttpGet("{nome}")]
        [ProducesResponseType(typeof(AnimalDto[]), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> ObterTodosPorNomeAsync(string nome)
            => Ok(await _animalService.ObterTodosPorNomeAsync(nome));

        /// <summary>
        /// Criar
        /// </summary>
        /// <param name="animal"></param>
        /// <response code="200">Criado com sucesso.</response>
        /// <response code="400">Não foi possível criar.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Post(AnimalDto animal)
        {
            if (!ModelState.IsValid)
                throw new QuironException("Os dados para criação são inválidos.");

            _animalService.Criar(animal);
            await _animalService.SalvarAlteracoesAsync();

            return Ok();
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <param name="animal"></param>
        /// <response code="200">Atualizado com sucesso.</response>
        /// <response code="400">Não foi possível atualizar.</response>
        /// <response code="404">Não localizado.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Put(AnimalDto animal)
        {
            if (!ModelState.IsValid)
                throw new QuironException("Os dados para atualização são inválidos.");

            if ((animal.Id.ToString().Equals("")) || (await _animalService.PesquisarPorIdAsync(animal.Id) == null))
                return NotFound();

            await _animalService.AtualizarAsync(animal);
            await _animalService.SalvarAlteracoesAsync();

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
            AnimalDto animal = await _animalService.PesquisarPorIdAsync(id);

            if (animal == null)
                return NotFound();

            await _animalService.RemoverAsync(animal.Id);
            await _animalService.SalvarAlteracoesAsync();

            return Ok();
        }
    }
}