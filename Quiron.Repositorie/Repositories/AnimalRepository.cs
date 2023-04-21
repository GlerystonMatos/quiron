using Quiron.Data.EF.Base;
using Quiron.Data.EF.Context;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;

namespace Quiron.Data.EF.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(QuironContext context) : base(context)
        {
        }
    }
}