using Microsoft.Extensions.DependencyInjection;
using Quiron.Domain.Interfaces.Services;
using Quiron.Service.Services;

namespace Quiron.CrossCutting
{
    public static class InjectorService
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}