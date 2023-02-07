using CarSpot.Api.Commands;
using CarSpot.Api.DTO;

namespace CarSpot.Api.Services
{
    public interface IReservationsService
    {
        /* ReservationDto Get(Guid id);
         IEnumerable<ReservationDto> GetAllWeekly();
         Guid? Create(CreateReservation command);
         bool Update(ChangeReservationLicencePlate command);
         bool Delete(DeleteReservation command);*/

        Task<ReservationDto> GetAsync(Guid id);
        Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync();
        Task<Guid?> CreateAsync(CreateReservation command);
        Task<bool> UpdateAsync(ChangeReservationLicencePlate command);
        Task<bool> DeleteAsync(DeleteReservation command);
    }
}
