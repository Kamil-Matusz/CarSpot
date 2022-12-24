using CarSpot.Api.Exceptions;
using System.Linq.Expressions;

namespace CarSpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _reservations = new();

        public Guid Id { get; }
        public DateTime From_Date { get; }
        public DateTime To_Date { get; }
        public string Spot_Name { get; }
        public IEnumerable<Reservation> Reservations => _reservations;

        public WeeklyParkingSpot(Guid id, DateTime from_Date,DateTime to_Date, string spot_Name) 
        {
            Id = id;
            From_Date = from_Date;
            To_Date = to_Date;
            Spot_Name = spot_Name;
        }
        public void AddReservation(Reservation reservation)
        {
            var now = DateTime.UtcNow.Date;
            if (reservation.ReservationDate.Date < From_Date || reservation.ReservationDate.Date > To_Date || reservation.ReservationDate < now.Date) 
            {
                throw new InvalidReservationDateException(reservation.ReservationDate);
            }

            var reservationAlreadyExist = _reservations.Any(x => x.ReservationDate.Date == reservation.ReservationDate.Date);

            if(reservationAlreadyExist)
            {
                throw new ParkingSpotAlreadyReservedException(Spot_Name, reservation.ReservationDate);
            }

            _reservations.Add(reservation); 
        }

      public void RemoveReservation(Guid ReservationId, Reservation reservation)
        {
            _reservations.Remove(reservation);
        }
    }
}
