using CarSpot.Api.ValueObject;

namespace CarSpot.Api.Commands
{
    public record ChangeReservationLicensePlate(Guid ReservationId,string LicensePlate);
}
