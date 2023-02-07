using CarSpot.Api.Entities;
using CarSpot.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        /* WeeklyParkingSpot Get(ParkingSpotId id);
         IEnumerable<WeeklyParkingSpot> GetAllWeekly();
         void Add(WeeklyParkingSpot weeklyParkingSpot);
         void Update(WeeklyParkingSpot weeklyParkingSpot);
         void Delete(WeeklyParkingSpot weeklyParkingSpot);*/
        Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id);
        Task<IEnumerable<WeeklyParkingSpot>> GetAllWeeklyAsync();
        Task AddAsync(WeeklyParkingSpot weeklyParkingSpot);
        Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot);
        Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot);

    }
}
