using NUnit.Framework;
using NUnit.Framework.Legacy;
using Quiron.Data.Dapper.Queries;
using Quiron.Domain.Entities;
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
        public async Task ObterTodosPorNomeTest()
        {
            TenantConfiguration configuration = _tenantService.Get();
            Cidade[] animais = await _cidadeQuery.ObterTodosPorNome(configuration.ConnectionStringDados, "Fortaleza");

            ClassicAssert.IsNotNull(animais.Where(a => a.Nome.Equals("Fortaleza")));
        }
    }
}