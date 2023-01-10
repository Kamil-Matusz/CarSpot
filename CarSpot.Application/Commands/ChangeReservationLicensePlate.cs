using System.Windows.Input;

namespace CarSpot.Api.Commands
{
    public record ChangeReservationLicencePlate(int ReservationId, string LicencePlate);
}
