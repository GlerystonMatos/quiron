using NUnit.Framework;
using Quiron.Data.Dapper.Queries;
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
        public void ObterTodosPorUfAsyncTest()
        {
            TenantConfiguration configuration = _tenantService.Get();
            Assert.ThatAsync(() => _estadoQuery.ObterTodosPorUfAsync(configuration.ConnectionStringDados, "CE"), Is.Not.Null);
        }
    }
}