using ElegantWebApi.Api.Extensions;
using ElegantWebApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.SetupMediatR();
builder.Services.SetupValidationForCommands();
builder.Services.SetupCustomHostedService();
builder.Services.SetupDictionaryAndDictionaryExpirationService();
builder.Services.ConfigureSwaggerDoc();
builder.Services.ConfigureCORS();
//Configure api key auth
builder.Services.AddScoped<ApiKeyAuthFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ValueListApi");
    });
}
app.UseCors("allowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
public partial class Program { }
