using CarSpot.Api.Entities;
using CarSpot.Core.Repositories;
using CarSpot.Core.ValueObject;
using CarSpot.Core.ValueObjects;
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

        /* public PostgresWeeklyParkingSpotRepository(CarSpotDbContext dbContext)
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

         public WeeklyParkingSpot Get(ParkingSpotId id) => _dbContext.WeeklyParkingSpots
             .Include(x => x.Reservations)
             .SingleOrDefault(x => x.WeeklyParkingSpotId== id);

         public IEnumerable<WeeklyParkingSpot> GetAllWeekly() => _dbContext.WeeklyParkingSpots
             .Include(x => x.Reservations)
             .ToList();

         public void Update(WeeklyParkingSpot weeklyParkingSpot)
         {
             _dbContext.Update(weeklyParkingSpot);
             _dbContext.SaveChanges();
         }*/

        public PostgresWeeklyParkingSpotRepository(CarSpotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            await _dbContext.AddAsync(weeklyParkingSpot);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Remove(weeklyParkingSpot);
            await _dbContext.SaveChangesAsync();
        }

        public Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id) => _dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.WeeklyParkingSpotId == id);

        public async Task<IEnumerable<WeeklyParkingSpot>> GetAllWeeklyAsync()
        {
            var result = await _dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .ToListAsync();

            return result.AsEnumerable();
        }

        public async Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _dbContext.Update(weeklyParkingSpot);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week) 
            => await _dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .Where(x => x.Week == week)
            .ToListAsync();
    }
}
