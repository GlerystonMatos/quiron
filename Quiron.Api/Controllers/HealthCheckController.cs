using Microsoft.AspNetCore.Mvc;

namespace Quiron.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        /// Verifica a saúde da API.
        /// </summary>
        /// <response code="200">API saudável</response>
        [HttpGet]
        [Route("Check")]
        public IActionResult Check()
            => Ok(new { Message = "Aplicacao Funcionando!" });
    }
}