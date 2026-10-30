using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using sgcae.Application;
using sgcae.Application.Exceptions;
using sgcae.Domain.Exceptions;
using sgcae.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

WebApplication app = builder.Build();

app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        int status = exception switch
        {
            BusinessRuleException => StatusCodes.Status400BadRequest,
            ConflictException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json";
        string message = status == StatusCodes.Status500InternalServerError
            ? "Ocurrió un error inesperado."
            : exception?.Message ?? "Ocurrió un error inesperado.";

        await context.Response.WriteAsJsonAsync(new { message });
    });
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

await DatabaseInitializer.MigrateAsync(app.Services);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
