using FleetGuard.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

string[] allowedOrigins =
    builder.Configuration
        .GetSection("AllowedOrigins")
        .Get<string[]>()
    ?? ["http://localhost:3000"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("FleetGuardFrontend", policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

string connectionString =
    builder.Configuration.GetConnectionString("FleetGuardDatabase")
    ?? "Data Source=fleetguard.db";

if (!builder.Environment.IsDevelopment())
{
    string homeDirectory =
        Environment.GetEnvironmentVariable("HOME")
        ?? builder.Environment.ContentRootPath;

    string dataDirectory =
        Path.Combine(homeDirectory, "data");

    Directory.CreateDirectory(dataDirectory);

    string databasePath =
        Path.Combine(dataDirectory, "fleetguard.db");

    connectionString = $"Data Source={databasePath}";
}

builder.Services.AddDbContext<FleetGuardDbContext>(options =>
    options.UseSqlite(connectionString));
var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    FleetGuardDbContext database =
        scope.ServiceProvider.GetRequiredService<FleetGuardDbContext>();

    database.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("FleetGuardFrontend");

app.MapControllers();

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    application = "FleetGuard API",
    timestamp = DateTime.UtcNow
}));

app.Run();