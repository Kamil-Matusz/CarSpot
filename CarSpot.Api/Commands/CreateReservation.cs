namespace CarSpot.Api.Commands
{
    public record CreateReservation(Guid ParkingSpotId,Guid ReservationId,string BookerName,string LicensePlate,DateTime ReservationDate);
}
