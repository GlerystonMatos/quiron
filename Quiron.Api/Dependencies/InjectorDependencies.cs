using Microsoft.Extensions.DependencyInjection;
using Quiron.Data.EF.Context;

namespace Quiron.Api.Dependencies
{
    public static class InjectorDependencies
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddDbContext<QuironContext>();
            services.RegisterRepository();
            services.RegisterService();
            services.RegisterQuery();
        }
    }
}