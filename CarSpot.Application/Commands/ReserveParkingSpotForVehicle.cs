using CarSpot.Application.Abstractions;

namespace CarSpot.Api.Commands
{
    //public sealed record ReserveParkingSpotForVehicle(Guid ParkingSpotId,Guid ReservationId,string BookerName,string LicensePlate,int Capacity,DateTime ReservationDate) : ICommand;
    public sealed record ReserveParkingSpotForVehicle(Guid ParkingSpotId, Guid ReservationId, Guid UserId,string LicencePlate, int Capacity, DateTime Date) : ICommand;
}
