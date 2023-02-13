using CarSpot.Api.Exceptions;
using CarSpot.Core.ValueObject;
using CarSpot.Core.ValueObjects;

namespace CarSpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private HashSet<Reservation> _weeklyParkingSpots = new();

        public ParkingSpotId WeeklyParkingSpotId { get;  set; }
       /* public DateTime FromDate { get; set; }
        public DateTime ToDate { get;  set; }*/
        public Week Week { get; private set; }
        public string ParkingSpotName { get; set; }
        public IEnumerable<Reservation> Reservations => _weeklyParkingSpots;

        public WeeklyParkingSpot(ParkingSpotId weeklyParkingSpotId, Week week, string parkingSpotName)
        {
            WeeklyParkingSpotId = weeklyParkingSpotId;
            Week = week;
            ParkingSpotName = parkingSpotName;
        }

        internal void AddReservation(Reservation reservation, Date now)
        {
            var isInvalidDate = reservation.ReservationDate < Week.From || reservation.ReservationDate > Week.To || reservation.ReservationDate < now;
            if(isInvalidDate)
            {
                throw new InvalidReservationDateException(reservation.ReservationDate.Value.Date);
            }

            var reservationAlreadyExists = Reservations.Any(x => x.ReservationDate == reservation.ReservationDate);
            if(reservationAlreadyExists)
            {
                throw new ParkingSpotAlreadyReservedException(ParkingSpotName,reservation.ReservationDate.Value.Date);
            }

            _weeklyParkingSpots.Add(reservation);
        }

        public void RemoveReservation(ReservationId reservationId) => _weeklyParkingSpots.RemoveWhere(x => x.ReservationId== reservationId);
        public void RemoveReservations(IEnumerable<Reservation> reservations)
        => _weeklyParkingSpots.RemoveWhere(x => reservations.Any(r => r.ReservationId == x.ReservationId));

    }
}
