using ElegantWebApi.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElegantWebApi.Api.Extensions
{
    public static class ApplicationConfigurationExtension
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

        public static IApplicationBuilder SetUpExcepions(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var logger = context.RequestServices.GetService<ILogger<Program>>();
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = (int)ValidErrorCode.GetErrorCode(error.Error);
                    context.Response.ContentType = "application/json";
                    var errorMessage = "Error occured";
                    if (error != null && error.Error is Exception)
                    {
                        errorMessage = error.Error.Message;
                    }

                    var response = new
                    {
                        errorCode = context.Response.StatusCode,
                        error = errorMessage
                    };
                    var json = JsonConvert.SerializeObject(response);
                    await context.Response.WriteAsync(json);
                    logger!.LogError(response.error, json);
                });
            });
            return app;
        }
    }
}