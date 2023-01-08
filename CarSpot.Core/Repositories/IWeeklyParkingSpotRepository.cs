using CarSpot.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        WeeklyParkingSpot Get(Guid id);
        IEnumerable<WeeklyParkingSpot> GetAllWeekly();
        void Add(WeeklyParkingSpot weeklyParkingSpot);
        void Update(WeeklyParkingSpot weeklyParkingSpot);
        void Delete(WeeklyParkingSpot weeklyParkingSpot);

    }
}
