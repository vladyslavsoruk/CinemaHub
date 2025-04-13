using cineHubBackend.DBContext;
using cineHubBackend.Interfaces;
using cineHubBackend.Models;
using cineHubBackend.Repositories;
using cineHubBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(ISessionRepository), typeof(SessionRepository));
builder.Services.AddScoped(typeof(ICinemaRepository), typeof(CinemaRepository));
builder.Services.AddScoped(typeof(IHallRepository), typeof(HallRepository));
builder.Services.AddScoped(typeof(ITicketRepository), typeof(TicketRepository));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IHallService, HallService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<CineDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<CineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowConfiguredOrigins",
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowConfiguredOrigins");
app.UseHttpsRedirection();
app.UseMiddleware<cineHubBackend.Exceptions.ExceptionMiddleware>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await SeedData.Initialize(services, configuration);
}

app.Run();