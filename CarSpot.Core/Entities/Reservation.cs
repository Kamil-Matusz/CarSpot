using CarSpot.Api.Exceptions;
using CarSpot.Core.ValueObject;
using CarSpot.Core.ValueObjects;

namespace CarSpot.Api.Entities
{
    public abstract  class Reservation
    {
        public ReservationId ReservationId { get; private set; }
        //public ParkingSpotId ParkingSpotId { get; private set; }
        public Capacity Capacity { get; private set; }
        public Date ReservationDate { get; private set; }

        protected Reservation() { }
        public Reservation(ReservationId reservationId,Capacity capacity ,Date reservationDate)
        {
            ReservationId = reservationId;
            Capacity = capacity;
            ReservationDate = reservationDate;
        }

    }
}
