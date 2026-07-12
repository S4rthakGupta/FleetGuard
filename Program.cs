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
    string azureDataDirectory = "/home/data";

    Directory.CreateDirectory(azureDataDirectory);

    connectionString =
        $"Data Source={Path.Combine(azureDataDirectory, "fleetguard.db")}";
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