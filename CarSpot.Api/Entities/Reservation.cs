namespace CarSpot.Api.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string BookerName { get; set; }
        public string ParkingSpotName { get; set; }
        public string LicensePlate { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
