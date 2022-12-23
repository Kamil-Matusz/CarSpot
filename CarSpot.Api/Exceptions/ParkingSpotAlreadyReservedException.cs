namespace CarSpot.Api.Exceptions
{
    public class ParkingSpotAlreadyReservedException : CustomException
    {
        public string Name { get; }
        public DateTime Date { get; }
        public ParkingSpotAlreadyReservedException(string name, DateTime date) : base("Parking spot already reserved")
        {
            Name = name;
            Date = date;
        }
    }
}
