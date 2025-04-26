using Microsoft.EntityFrameworkCore;
using PokyBack.Rooms.App.Handlers;
using PokyBack.Rooms.Core.Abstractions;
using PokyBack.Rooms.Infrastructure.Repositories;
using PokyBack.Shared.Core.Abstractions;
using PokyBack.Shared.Core.Persistence;
using PokyBack.Shared.Infrastructure.Persistence;
using PokyBack.Shared.Infrastructure.Repositories;
using PokyBack.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSignalR(config =>
{
    config.EnableDetailedErrors = true;
    config.HandshakeTimeout = TimeSpan.FromMinutes(40);
    config.ClientTimeoutInterval = TimeSpan.FromMinutes(40);
    config.KeepAliveInterval = TimeSpan.FromMinutes(40);
}).AddJsonProtocol();

// Registering model builder delegates
builder.Services.AddSingleton<Action<ModelBuilder>>(_ =>
{
    return ConfigureAction;

    void ConfigureAction(ModelBuilder mb)
    {
        mb.ApplyConfigurationsFromAssembly(typeof(RoomEntityTypeConfiguration).Assembly);
    }
});

// Adding DbContext
builder.Services.AddDbContext<AppDbContext>((_, options) =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("SqliteDev"),
        b => b.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)
    ).UseLazyLoadingProxies();
});

// Adding MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(GetAllRoomsQueryHandler).Assembly);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "corsPolicies",
        policy  =>
        {
            policy.WithOrigins(
                    "http://localhost:3000"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});


// Dependency injection
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomUserRepository, RoomUserRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseCors("corsPolicies");

app.MapControllers();
app.MapHub<PokyHub>("/_ws");

app.Run();