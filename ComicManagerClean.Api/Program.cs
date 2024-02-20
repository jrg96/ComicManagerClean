using Asp.Versioning;
using Asp.Versioning.Conventions;
using ComicManagerClean.Api.Middleware;
using ComicManagerClean.Api.Swagger;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog configuration
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.OperationFilter<SwaggerDefaultValues>();
});

var app = builder.Build();

// Serilog, Exception MiddleWare
var versionSet = app.NewApiVersionSet()
    .HasApiVersion(1)
    .ReportApiVersions()
    .Build();
app.UseSerilogRequestLogging();
app.UseExceptionHandleMiddleware();

app.UseHttpsRedirection();

app.MapGet("/api/v{version:apiVersion}/weatherforecast", () =>
{
    return "Hello World!";
})
.WithApiVersionSet(versionSet)
.MapToApiVersion(1);


// Configure the HTTP request pipeline.
// NOTE: Swagger config should come after all endpoints have been defined
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.Run();
