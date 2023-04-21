using Quiron.Data.EF.Base;
using Quiron.Data.EF.Context;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Data;

namespace Quiron.Data.EF.Repositories
{
    public class EstadoRepository : Repository<Estado>, IEstadoRepository
    {
        public EstadoRepository(QuironContext context) : base(context)
        {
        }
    }
}