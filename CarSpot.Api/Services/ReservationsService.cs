using CarSpot.Api.Commands;
using CarSpot.Api.DTO;
using CarSpot.Api.Entities;
using CarSpot.Api.ValueObject;
using System.Linq;

namespace CarSpot.Api.Services
{
    public class ReservationsService
    {
        private static Clock _clock = new();
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots = new()
        {
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(_clock.CurrentDate()),"P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(_clock.CurrentDate()),"P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(_clock.CurrentDate()),"P3"),
        };

        public ReservationDto Get(ReservationId id) => GetAllWeekly().SingleOrDefault(x => x.ReservationDtoId== id);
        

        public IEnumerable<ReservationDto> GetAllWeekly()
        {
            return _weeklyParkingSpots.SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
                {
                   ReservationDtoId= x.ReservationId,
                   ParkingSpotId= x.ParkingSpotId,
                   BookerName= x.BookerName,
                   ReservationDate= x.ReservationDate,
                });
        }

        public Guid? Create(CreateReservation command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.Id==parkingSpotId);
            if(weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId,command.BookerName, command.LicensePlate, new Date(command.ReservationDate));
            weeklyParkingSpot.AddReservation(reservation, new Date(_clock.CurrentDate()));

            return reservation.ReservationId;
        }

        public bool Update(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.ReservationId == reservationId);
            if (existingReservation is null)
            {
                return false;
            }

            if(existingReservation.ReservationDate.Value.Date <= _clock.CurrentDate())
            {
                return false;
            }

            existingReservation.ChangeLicensePlate(command.LicensePlate);
            return true;
        }

        public bool Delete(DeleteReservation command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                return false;
            }

            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.ReservationId == reservationId);
            if (existingReservation is null)
            {
                return false;
            }
            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId reservationId) => _weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));
    }
}
