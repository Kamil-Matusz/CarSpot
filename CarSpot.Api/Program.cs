using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using CarSpot.Application;
using CarSpot.Infrastructure;
using CarSpot.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddSingleton<IClock,Clock>()
    .AddSingleton<IEnumerable<WeeklyParkingSpot>>(serviceProvider =>
    {
        var clock = serviceProvider.GetRequiredService<IClock>();
        return new List<WeeklyParkingSpot>()
    {
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), clock.CurrentDate(), clock.CurrentDate().AddDays(7), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), clock.CurrentDate(), clock.CurrentDate().AddDays(7), "P2"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), clock.CurrentDate(), clock.CurrentDate().AddDays(7), "P3"),
    };
    })
    //.AddScoped<IReservationsService,ReservationsService>()
    .AddApplication()
    .AddInfrastructure()
    .AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<CarSpotDbContext>();
    dbContext.Database.Migrate();
}


app.Run();
