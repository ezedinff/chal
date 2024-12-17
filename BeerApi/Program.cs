using BeerApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using BeerApi.Validators;
using BeerApi.Services;
using BeerApi.Mappings;
using BeerApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBeerDtoValidator>();

builder.Services.AddDbContext<BeerDbContext>(options =>
    options.UseInMemoryDatabase("BeerDb"));

builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddAutoMapper(typeof(BeerMappingProfile));
builder.Services.AddScoped<IBeerRepository, BeerRepository>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Beer API", 
        Version = "v1",
        Description = "A simple API for managing beer collection"
    });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure Swagger UI at root
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Beer API V1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the root URL
});

// Redirect root to Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
