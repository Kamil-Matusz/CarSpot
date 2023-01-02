using CarSpot.Api.Commands;
using CarSpot.Api.DTO;

namespace CarSpot.Api.Services
{
    public interface IReservationsService
    {
        ReservationDto Get(Guid id);
        IEnumerable<ReservationDto> GetAllWeekly();
        Guid? Create(CreateReservation command);
        bool Update(ChangeReservationLicencePlate command);
        bool Delete(DeleteReservation command);
    }
}
