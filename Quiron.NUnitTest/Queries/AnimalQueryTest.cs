using NUnit.Framework;
using Quiron.Data.Dapper.Queries;
using Quiron.Domain.Entities;
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
        public async Task ObterTodosPorNomeTest()
        {
            TenantConfiguration configuration = _tenantService.Get();
            Animal[] animais = await _animalQuery.ObterTodosPorNome(configuration.ConnectionStringDados, "Cachorro");

            Assert.IsNotNull(animais.Where(a => a.Nome.Equals("Cachorro")));
        }
    }
}