using CarSpot.Api.Exceptions;
using CarSpot.Core.ValueObject;
using CarSpot.Core.ValueObjects;

namespace CarSpot.Api.Entities
{
    public abstract  class Reservation
    {
        public ReservationId ReservationId { get; private set; }
        public ParkingSpotId ParkingSpotId { get; private set; }
       /* public BookerName BookerName { get; private set; }
        public string LicensePlate { get; private set; }*/
        public Capacity Capacity { get; private set; }
        public Date ReservationDate { get; private set; }

        protected Reservation() { }
        public Reservation(ReservationId reservationId,ParkingSpotId parkingSpotId,Capacity capacity ,Date reservationDate)
        {
            ReservationId = reservationId;
            ParkingSpotId= parkingSpotId;
            Capacity = capacity;
            ReservationDate = reservationDate;
        }

    }
}
