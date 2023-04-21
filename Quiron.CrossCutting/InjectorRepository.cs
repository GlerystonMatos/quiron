using Microsoft.Extensions.DependencyInjection;
using Quiron.Data.EF.Repositories;
using Quiron.Domain.Interfaces.Data;

namespace Quiron.CrossCutting
{
    public static class InjectorRepository
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}