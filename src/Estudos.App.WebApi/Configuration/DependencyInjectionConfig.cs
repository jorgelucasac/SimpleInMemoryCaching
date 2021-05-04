using Estudos.App.WebApi.Services;
using Estudos.App.WebApi.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Estudos.App.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IPaisService, PaisService>();
            services.AddScoped<IResponseCacheService, ResponseCacheService>();
        }
    }
}