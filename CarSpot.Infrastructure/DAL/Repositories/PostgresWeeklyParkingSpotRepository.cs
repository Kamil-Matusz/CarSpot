using CarSpot.Api.Entities;
using CarSpot.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL.Repositories
{
    internal sealed class PostgresWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly CarSpotDbContext _dbContext;

        public PostgresWeeklyParkingSpotRepository(CarSpotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Add(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }

        public void Delete(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Remove(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }

        public WeeklyParkingSpot Get(Guid id) => _dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefault(x => x.WeeklyParkingSpotId== id);

        public IEnumerable<WeeklyParkingSpot> GetAllWeekly() => _dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .ToList();

        public void Update(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Update(weeklyParkingSpot);
            _dbContext.SaveChanges();
        }
    }
}
