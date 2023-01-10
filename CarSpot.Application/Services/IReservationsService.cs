using CarSpot.Api.Commands;
using CarSpot.Api.DTO;

namespace CarSpot.Api.Services
{
    public interface IReservationsService
    {
        ReservationDto Get(int id);
        IEnumerable<ReservationDto> GetAllWeekly();
        int? Create(CreateReservation command);
        bool Update(ChangeReservationLicencePlate command);
        bool Delete(DeleteReservation command);
    }
}
