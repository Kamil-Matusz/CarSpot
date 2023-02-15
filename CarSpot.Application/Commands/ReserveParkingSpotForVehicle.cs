using CarSpot.Application.Abstractions;

namespace CarSpot.Api.Commands
{
    public sealed record ReserveParkingSpotForVehicle(Guid ParkingSpotId,Guid ReservationId,string BookerName,string LicensePlate,int Capacity,DateTime ReservationDate) : ICommand;
}
