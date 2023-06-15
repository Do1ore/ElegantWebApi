using ElegantWebApi.Application.Features.AddDataList;
using ElegantWebApi.Infrastructure;
using ElegantWebApi.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace ElegantWebApi.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Application.Features.AddDataList.AddDataListHandler).Assembly);

            return services;
        }

        public static IServiceCollection SetupValidationForCommands(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AddDataListCommand>, AddDataListCommandValidator>();
            return services;
        }

        public static IServiceCollection SetupCustomHostedService(this IServiceCollection services)
        {
            services.AddHostedService<ConcurrentDictionaryHostedService>();
            return services;
        }
    }
}
