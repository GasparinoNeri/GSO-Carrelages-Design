using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Core.UseCases;
using GsoCarrelages.Core.UseCases.Abstractions;
using GsoCarrelages.Infrastructure.Data;
using GsoCarrelages.Infrastructure.Gateways;
using GsoCarrelages.Infrastructure.Repositories;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "La chaîne de connexion DefaultConnection est manquante."
    );
}

builder.Services.AddSingleton(
    new DbConnectionFactory(connectionString)
);

// UseCases
builder.Services.AddScoped<IProductUseCases, ProductUseCases>();
builder.Services.AddScoped<IAuthUseCases, AuthUseCases>();

// Gateways
builder.Services.AddScoped<IProductGateway, ProductGateway>();
builder.Services.AddScoped<IUserGateway, UserGateway>();

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AngularPolicy");
app.MapControllers();

app.Run();