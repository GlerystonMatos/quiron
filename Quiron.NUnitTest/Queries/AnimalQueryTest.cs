using NUnit.Framework;
using Quiron.Data.Dapper.Queries;
using Quiron.Domain.Interfaces.Queries;
using Quiron.Domain.Interfaces.Services;
using Quiron.Domain.Tenant;
using Quiron.NUnitTest.Utilitarios;

namespace Quiron.NUnitTest.Queries
{
    public class AnimalQueryTest
    {
        private IAnimalQuery _animalQuery;
        private ITenantService _tenantService;

        public AnimalQueryTest()
        {
            _tenantService = Tenant.Get();
            _animalQuery = new AnimalQuery();
        }

        [Test]
        public void ObterTodosPorNomeAsyncTest()
        {
            TenantConfiguration configuration = _tenantService.Get();
            Assert.ThatAsync(() => _animalQuery.ObterTodosPorNomeAsync(configuration.ConnectionStringDados, "Cachorro"), Is.Not.Null);
        }
    }
}