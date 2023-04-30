using NUnit.Framework;
using Quiron.Data.Dapper.Queries;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Queries;
using Quiron.Domain.Interfaces.Services;
using Quiron.Domain.Tenant;
using Quiron.NUnitTest.Utilitarios;

namespace Quiron.NUnitTest.Queries
{
    public class EstadoQueryTest
    {
        private IEstadoQuery _estadoQuery;
        private ITenantService _tenantService;

        public EstadoQueryTest()
        {
            _tenantService = Tenant.Get();
            _estadoQuery = new EstadoQuery();
        }

        [Test]
        public async Task ObterTodosPorNomeTest()
        {
            TenantConfiguration configuration = _tenantService.Get();
            Estado[] animais = await _estadoQuery.ObterTodosPorUf(configuration.ConnectionStringDados, "CE");

            Assert.IsNotNull(animais.Where(a => a.Nome.Equals("Ceará")));
        }
    }
}