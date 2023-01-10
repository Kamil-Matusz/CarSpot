namespace CarSpot.Api.DTO
{
    public class ReservationDto
    {
        public int ReservationDtoId { get; set; }
        public int ParkingSpotId { get; set; }
        public string BookerName { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
