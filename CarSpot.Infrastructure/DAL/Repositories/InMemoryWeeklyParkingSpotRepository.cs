using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using CarSpot.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Repositories
{
        internal class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
        {
        private readonly IClock _clock;
        private List<WeeklyParkingSpot> _weeklyParkingSpots = new List<WeeklyParkingSpot>();

        public InMemoryWeeklyParkingSpotRepository(IClock clock)
        {
            _clock= clock;
            _weeklyParkingSpots = new List<WeeklyParkingSpot>()
            {
            new WeeklyParkingSpot(1, _clock.CurrentDate(), _clock.CurrentDate().AddDays(7), "P1"),
            new WeeklyParkingSpot(2, _clock.CurrentDate(), _clock.CurrentDate().AddDays(7), "P2"),
            new WeeklyParkingSpot(3, _clock.CurrentDate(), _clock.CurrentDate().AddDays(7), "P3"),
            };
        }
        public void Add(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Add(weeklyParkingSpot);

        public void Delete(WeeklyParkingSpot weeklyParkingSpot)
        {
            throw new NotImplementedException();
        }

        public WeeklyParkingSpot Get(int id) => _weeklyParkingSpots.SingleOrDefault(x => x.WeeklyParkingSpotId== id);

        public IEnumerable<WeeklyParkingSpot> GetAllWeekly() => _weeklyParkingSpots;

        public void Update(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Remove(weeklyParkingSpot);
    }
}
