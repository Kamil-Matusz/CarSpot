using CarSpot.Api.Exceptions;
using CarSpot.Api.ValueObject;

namespace CarSpot.Api.Entities
{
    public class Reservation
    {
        public ReservationId ReservationId { get; private set; }
        public ParkingSpotId ParkingSpotId { get; private set; }
        public BookerName BookerName { get; private set; }
        public LicensePlate LicensePlate { get; private set; }
        public Date ReservationDate { get; private set; }

        public Reservation(ReservationId reservationId,ParkingSpotId parkingSpotId, BookerName bookerName, LicensePlate licensePlate, Date reservationDate)
        {
            ReservationId = reservationId;
            ParkingSpotId= parkingSpotId;
            BookerName = bookerName;
            ChangeLicensePlate(licensePlate);
            ReservationDate= reservationDate;
        }

        public void ChangeLicensePlate(LicensePlate licensePlate) => LicensePlate = licensePlate;
    }
}
