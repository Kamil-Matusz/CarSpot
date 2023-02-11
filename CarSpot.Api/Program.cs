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
    .AddInfrastructure(builder.Configuration)
    .AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// evoking custom middleware for exception
app.UseInfrastructure();

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


app.Run();
