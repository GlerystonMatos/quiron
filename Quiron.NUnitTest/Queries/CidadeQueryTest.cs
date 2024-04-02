using NUnit.Framework;
using Quiron.Data.Dapper.Queries;
using Quiron.Domain.Interfaces.Queries;
using Quiron.Domain.Interfaces.Services;
using Quiron.Domain.Tenant;
using Quiron.NUnitTest.Utilitarios;

namespace Quiron.NUnitTest.Queries
{
    public class CidadeQueryTest
    {
        private ICidadeQuery _cidadeQuery;
        private ITenantService _tenantService;

        public CidadeQueryTest()
        {
            _tenantService = Tenant.Get();
            _cidadeQuery = new CidadeQuery();
        }

        [Test]
        public void ObterTodosPorNomeAsyncTest()
        {
            TenantConfiguration configuration = _tenantService.Get();
            Assert.ThatAsync(() => _cidadeQuery.ObterTodosPorNomeAsync(configuration.ConnectionStringDados, "Fortaleza"), Is.Not.Null);
        }
    }
}