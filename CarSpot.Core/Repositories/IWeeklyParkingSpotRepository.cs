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
        WeeklyParkingSpot Get(int id);
        IEnumerable<WeeklyParkingSpot> GetAllWeekly();
        void Add(WeeklyParkingSpot weeklyParkingSpot);
        void Update(WeeklyParkingSpot weeklyParkingSpot);
        void Delete(WeeklyParkingSpot weeklyParkingSpot);

    }
}
