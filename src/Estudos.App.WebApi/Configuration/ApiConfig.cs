using Microsoft.Extensions.DependencyInjection;

namespace Estudos.App.WebApi.Configuration
{
    public static class ApiConfig
    {
        public static void WebApiConfig(this IServiceCollection services)
        {
            services.AddControllers();

            #region Cosrs

            services.AddCors(opt =>
            {
                opt.AddPolicy("Development",
                    builder =>
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

            });

            #endregion
        }
    }
}