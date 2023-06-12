using MediatR;

namespace ElegantWebApi.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Application.Features.AddDataList.AddDataListHandler).Assembly);

            return services;
        }
    }
}
