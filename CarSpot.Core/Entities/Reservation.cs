using CarSpot.Api.Exceptions;
using CarSpot.Core.ValueObject;

namespace CarSpot.Api.Entities
{
    public class Reservation
    {
        public Guid ReservationId { get; private set; }
        public ParkingSpotId ParkingSpotId { get; private set; }
        public string BookerName { get; private set; }
        public string LicensePlate { get; private set; }
        public DateTime ReservationDate { get; private set; }

        public Reservation(Guid reservationId,ParkingSpotId parkingSpotId, string bookerName, string licensePlate, DateTime reservationDate)
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
