using CarSpot.Api.Exceptions;

namespace CarSpot.Api.Entities
{
    public class Reservation
    {
        public Guid ReservationId { get; private set; }
        public Guid ParkingSpotId { get; private set; }
        public string BookerName { get; private set; }
        public string LicensePlate { get; private set; }
        public DateTime ReservationDate { get; private set; }

        public Reservation(Guid reservationId,Guid parkingSpotId, string bookerName, string licensePlate, DateTime reservationDate)
        {
            ReservationId = reservationId;
            ParkingSpotId= parkingSpotId;
            BookerName = bookerName;
            ChangeLicensePlate(licensePlate);
            ReservationDate = reservationDate;
        }

        public void ChangeLicensePlate(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                throw new InvalidLicencePlateException(licensePlate);
            }

            LicensePlate = licensePlate;

        }
    }
}
