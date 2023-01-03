using CarSpot.Api.Exceptions;

namespace CarSpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _weeklyParkingSpots = new();

        public Guid WeeklyParkingSpotId { get;  }
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }
        public string ParkingSpotName { get; private set; }
        public IEnumerable<Reservation> Reservations => _weeklyParkingSpots;

        public WeeklyParkingSpot(Guid weeklyParkingSpotId, DateTime fromDate, DateTime toDate, string parkingSpotName)
        {
            WeeklyParkingSpotId = weeklyParkingSpotId;
            FromDate = fromDate;
            ToDate = toDate;
            ParkingSpotName = parkingSpotName;
        }

        public void AddReservation(Reservation reservation, DateTime now)
        {
            var isInvalidDate = reservation.ReservationDate.Date < FromDate || reservation.ReservationDate.Date > ToDate || reservation.ReservationDate.Date < now;
            if(isInvalidDate)
            {
                throw new InvalidReservationDateException(reservation.ReservationDate);
            }

            var reservationAlreadyExists = Reservations.Any(x => x.ReservationDate.Date == reservation.ReservationDate.Date);
            if(reservationAlreadyExists)
            {
                throw new ParkingSpotAlreadyReservedException(ParkingSpotName,reservation.ReservationDate);
            }

            _weeklyParkingSpots.Add(reservation);
        }

        public void RemoveReservation(Guid reservationId) => _weeklyParkingSpots.RemoveWhere(x => x.ReservationId== reservationId);

    }
}
