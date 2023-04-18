using Quiron.Data.Base;
using Quiron.Data.Context;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;

namespace Quiron.Data.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(QuironContext context) : base(context)
        {
        }
    }
}