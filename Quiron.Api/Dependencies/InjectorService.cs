using Microsoft.Extensions.DependencyInjection;
using Quiron.Domain.Interfaces.Services;
using Quiron.Service.Services;

namespace Quiron.Api.Dependencies
{
    public static class InjectorService
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<ICidadeService, CidadeService>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}