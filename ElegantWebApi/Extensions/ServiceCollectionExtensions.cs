using ElegantWebApi.Application.Features.AddDataList;
using ElegantWebApi.Application.Features.AppendValue;
using ElegantWebApi.Application.Features.DeleteDataList;
using ElegantWebApi.Application.Features.UpdateDataList;
using ElegantWebApi.Infrastructure.Contracts;
using ElegantWebApi.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
            services.AddTransient<IValidator<AppendValueCommand>, AppendValueCommandValidator>();
            services.AddTransient<IValidator<DeleteRecordListCommand>, DeleteRecordCommandValidator>();

            return services;
        }
        public static IServiceCollection SetupDictionaryAndDictionaryExpirationService(this IServiceCollection services)
        {
            services.AddSingleton<IConcurrentDictionaryService, ConcurrentDictionaryService>();
            services.AddSingleton<IExprirationDataService, ExpirationDataService>();
            return services;
        }

        public static IServiceCollection SetupCustomHostedService(this IServiceCollection services)
        {
            services.AddHostedService<ConcurrentDictionaryHostedService>();
            return services;
        }

        public static IServiceCollection ConfigureSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ValueListApi", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile );

                c.IncludeXmlComments(xmlPath);
            });
         
            return services;
        }
    }
}
