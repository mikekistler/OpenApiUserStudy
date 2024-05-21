using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Don't serialize null values
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Convert exceptions to problem details responses
// ref: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling#problem-details
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi(options =>
{
    options.UseTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "Todo API",
            Version = "1.0.0",
            Description = "An ASP.NET Core Web API for Todo lists.",
            Contact = new OpenApiContact
            {
                Name = "Contoso API Admin",
                Url = new Uri("https://contoso.com/admin")
            }
        };
        document.Servers.Add(new OpenApiServer { Url = "https://localhost:7195", Description = "Localhost" });
        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.MapTodos();

app.MapOpenApi();

app.Run();
