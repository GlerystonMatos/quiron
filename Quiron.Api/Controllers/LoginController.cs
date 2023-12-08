using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quiron.Api.Security;
using Quiron.Domain.Dto;
using Quiron.Domain.Exception;
using Quiron.Domain.Interfaces.Services;
using Quiron.Domain.Tenant;
using System.Linq;
using System.Threading.Tasks;

namespace Quiron.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<LoginController> _logger;
        private readonly IOptions<TenantConfigurationSection> _options;

        public LoginController(ITenantService tenantService, IUsuarioService usuarioService,
            ILogger<LoginController> logger, IOptions<TenantConfigurationSection> options)
        {
            _logger = logger;
            _options = options;
            _tenantService = tenantService;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Autenticar usuário
        /// </summary>
        /// <param name="login"></param>
        /// <response code="200">Usuário autenticado.</response>
        /// <response code="400">Usuário não autenticado.</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("Authenticate")]
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto login)
        {
            TenantConfiguration tenant = _options.Value.Tenants.Where(t => t.Name.Equals(login.Tenant)).SingleOrDefault();
            if (tenant == null)
                tenant = _options.Value.Tenants.Where(t => t.Name.Equals("Tenant00")).SingleOrDefault();

            _tenantService.Set(tenant);
            _tenantService.SetUser(login.Login);
            _logger.LogInformation("User: " + login.Login);
            _logger.LogInformation("Tenant: " + tenant.Name);

            UsuarioDto usuarioDto = await _usuarioService.ObterUsuarioParaAutenticacao(login);
            _logger.LogInformation("Login realizado: " + usuarioDto.Nome);

            return Ok(new TokenDto(AccessToken.GenerateToken(usuarioDto, tenant.Name)));
        }

        /// <summary>
        /// Verificar o usuário autenticado.
        /// </summary>
        /// <response code="200">Usuário autenticado.</response>
        /// <response code="400">Usuário não autenticado.</response>
        [HttpGet]
        [Authorize]
        [Route("Authenticated")]
        [ProducesResponseType(typeof(ExceptionMessage), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Authenticated()
        {
            TenantConfiguration configuration = _tenantService.Get();
            _logger.LogInformation("Tenant: " + configuration.Name);
            return Ok(new ExceptionMessage(string.Format("Usuário autenticado - {0}", User.Identity.Name)));
        }
    }
}