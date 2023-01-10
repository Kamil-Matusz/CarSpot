namespace CarSpot.Api.Commands
{
    public record CreateReservation(int ParkingSpotId,int ReservationId,string BookerName,string LicensePlate,DateTime ReservationDate);
}
