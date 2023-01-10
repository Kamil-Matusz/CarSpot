using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using CarSpot.Application;
using CarSpot.Core;
using CarSpot.Infrastructure;
using CarSpot.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure()
    .AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// auto migration if don't exist
using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<CarSpotDbContext>();
    dbContext.Database.Migrate();

    var weeklyParkingSpot = dbContext.WeeklyParkingSpots.ToList();
    if(!weeklyParkingSpot.Any())
    {
        var clock = new Clock();
        weeklyParkingSpot = new List<WeeklyParkingSpot>()
            {
            new WeeklyParkingSpot(1, clock.CurrentDate(), clock.CurrentDate().AddDays(7), "P1"),
            new WeeklyParkingSpot(2, clock.CurrentDate(), clock.CurrentDate().AddDays(7), "P2"),
            new WeeklyParkingSpot(3, clock.CurrentDate(), clock.CurrentDate().AddDays(7), "P3"),
            };
        dbContext.AddRange(weeklyParkingSpot);
        dbContext.SaveChanges();
    }
}


app.Run();
