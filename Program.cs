using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Don't serialize null values
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapTodos();

app.Run();
