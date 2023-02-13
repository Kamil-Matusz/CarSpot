 namespace CarSpot.Api.Commands
{
    public record ReserveParkingSpotForVehicle(Guid ParkingSpotId,Guid ReservationId,string BookerName,string LicensePlate,DateTime ReservationDate);
}
