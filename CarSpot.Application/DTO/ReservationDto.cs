namespace CarSpot.Api.DTO
{
    public class ReservationDto
    {
        public Guid ReservationDtoId { get; set; }
        public Guid ParkingSpotId { get; set; }
        public string BookerName { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
