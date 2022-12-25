using CarSpot.Api.Exceptions;
using CarSpot.Api.ValueObject;
using System.Linq.Expressions;

namespace CarSpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _reservations = new();

        public ParkingSpotId Id { get; }
        
        public Week Week { get; }
        public string SpotName { get; }
        public IEnumerable<Reservation> Reservations => _reservations;

        public WeeklyParkingSpot(ParkingSpotId id, Week week, string spot_Name) 
        {
            Id = id;
            Week = week;
            SpotName = spot_Name;
        }
        public void AddReservation(Reservation reservation, Date now)
        {
            if (reservation.ReservationDate < Week.From_Date || reservation.ReservationDate > Week.To_Date || reservation.ReservationDate < now) 
            {
                throw new InvalidReservationDateException(reservation.ReservationDate.Value.Date);
            }

            var reservationAlreadyExist = _reservations.Any(x => x.ReservationDate == reservation.ReservationDate);

            if(reservationAlreadyExist)
            {
                throw new ParkingSpotAlreadyReservedException(SpotName, reservation.ReservationDate.Value.Date);
            }

            _reservations.Add(reservation); 
        }

        public void RemoveReservation(ReservationId reservationId)
        {
            _reservations.RemoveWhere(x => x.ReservationId== reservationId);
        }
    }
}
