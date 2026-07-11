using FleetGuard.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

string databasePath = Path.Combine(
    builder.Environment.ContentRootPath,
    "fleetguard.db");

builder.Services.AddDbContext<FleetGuardDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath}"));

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

app.MapControllers();

app.Run();