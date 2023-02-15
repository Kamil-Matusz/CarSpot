using CarSpot.Application.Abstractions;

namespace CarSpot.Api.Commands
{
    public record ChangeReservationLicencePlate(Guid ReservationId, string LicencePlate) : ICommand;
}
