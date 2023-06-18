using Microsoft.AspNetCore.Mvc;

namespace ElegantWebApi.Api.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection SetUpApiDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressMapClientErrors = true; // Отключение обработки ошибок клиента
            });

            return services;
        }
    }
}
