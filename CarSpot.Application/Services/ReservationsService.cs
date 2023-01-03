using CarSpot.Api.Commands;
using CarSpot.Api.DTO;
using CarSpot.Api.Entities;

namespace CarSpot.Api.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IClock _clock;
        private readonly IEnumerable<WeeklyParkingSpot> _weeklyParkingSpots;
        public ReservationsService(IClock clock, IEnumerable<WeeklyParkingSpot> weeklyParkingSpots)
        {
            _clock = clock;
            _weeklyParkingSpots = weeklyParkingSpots;
        }

        public ReservationDto Get(Guid id)
        {
            return GetAllWeekly().SingleOrDefault(x => x.ReservationDtoId== id);
        }

        //public IEnumerable<Reservation> GetAll() 
        //{
        //    return _reservations;
        //}

        public IEnumerable<ReservationDto> GetAllWeekly() => _weeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto
            {
                ReservationDtoId = x.ReservationId,
                ParkingSpotId = x.ParkingSpotId,
                BookerName = x.BookerName,
                ReservationDate = x.ReservationDate
            });
        

        public Guid? Create(CreateReservation command)
        {   
            var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.WeeklyParkingSpotId == command.ParkingSpotId);
            if(weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.BookerName, command.LicensePlate, command.ReservationDate);
            weeklyParkingSpot.AddReservation(reservation,_clock.CurrentDate());

            return reservation.ReservationId;
        }

        public bool Update(ChangeReservationLicencePlate command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if(weeklyParkingSpot is null)
            {
                return false;
            }

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.ReservationId == command.ReservationId);
            if (existingReservation is null)
            {
                return false;
            }

            if(existingReservation.ReservationDate <= _clock.CurrentDate())
            {
                return false;
            }

            existingReservation.ChangeLicensePlate(command.LicencePlate);
            return true;
        }

        public bool Delete(DeleteReservation command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if(weeklyParkingSpot is null)
            {
                return false;
            }
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.ReservationId == command.ReservationId);
            if (existingReservation is null)
            {
                return false;
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId) => _weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));
    }
}
