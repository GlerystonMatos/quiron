using Microsoft.Extensions.DependencyInjection;
using Quiron.Data.Dapper.Queries;
using Quiron.Domain.Interfaces.Queries;

namespace Quiron.Api.Dependencies
{
    public static class InjectorQuery
    {
        public static void RegisterQuery(this IServiceCollection services)
        {
            services.AddScoped<IAnimalQuery, AnimalQuery>();
            services.AddScoped<IEstadoQuery, EstadoQuery>();
            services.AddScoped<ICidadeQuery, CidadeQuery>();
        }
    }
}