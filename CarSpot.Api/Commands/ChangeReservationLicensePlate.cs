using System.Windows.Input;

namespace CarSpot.Api.Commands
{
    public record ChangeReservationLicencePlate(Guid ReservationId, string LicencePlate);
}
