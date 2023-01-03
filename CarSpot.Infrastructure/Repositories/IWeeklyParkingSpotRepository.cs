using CarSpot.Api.Entities;

namespace CarSpot.Api.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        IEnumerable<WeeklyParkingSpot> GetAllWeekly();
        WeeklyParkingSpot Get(Guid id);
        void Add(WeeklyParkingSpot weeklyParkingSpot);
        void Update(WeeklyParkingSpot weeklyParkingSpot);
        void Delete(WeeklyParkingSpot weeklyParkingSpot);
    }
}
