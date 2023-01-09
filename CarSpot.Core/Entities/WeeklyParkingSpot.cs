using CarSpot.Api.Exceptions;
using CarSpot.Core.ValueObject;

namespace CarSpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private HashSet<Reservation> _weeklyParkingSpots = new();

        public ParkingSpotId WeeklyParkingSpotId { get;  set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get;  set; }
        public string ParkingSpotName { get; set; }
        public IEnumerable<Reservation> Reservations => _weeklyParkingSpots;

        public WeeklyParkingSpot(ParkingSpotId weeklyParkingSpotId, DateTime fromDate, DateTime toDate, string parkingSpotName)
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
