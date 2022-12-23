using CarSpot.Api.Exceptions;

namespace CarSpot.Api.Entities
{
    public class Reservation
    {
        public int ReservationId { get; private set; }
        public string BookerName { get; private set; }
        public string LicensePlate { get; private set; }
        public DateTime ReservationDate { get; private set; }

        public Reservation(int reservationId, string bookerName, string licensePlate, DateTime reservationDate)
        {
            ReservationId = reservationId;
            BookerName = bookerName;
           ChangeLicensePlate(licensePlate);
            ReservationDate = reservationDate; 
        }

        public void ChangeLicensePlate(string licensePlate)
        {
            if(string.IsNullOrWhiteSpace(licensePlate))
            {
                throw new EmptyLicensePlateException();
            }

            LicensePlate = licensePlate;
        }
    }
}
