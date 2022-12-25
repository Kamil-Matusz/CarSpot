using CarSpot.Api.ValueObject;

namespace CarSpot.Api.DTO
{
    public class ReservationDto
    {
        public ReservationId ReservationDtoId { get; set; }
        public ParkingSpotId ParkingSpotId { get; set; }
        public BookerName BookerName { get; set; }
        public Date ReservationDate { get; set; }
    }
}
