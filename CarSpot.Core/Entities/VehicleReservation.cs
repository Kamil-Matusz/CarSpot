using CarSpot.Api.Entities;
using CarSpot.Api.Exceptions;
using CarSpot.Core.ValueObject;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Entities
{
    public sealed class VehicleReservation : Reservation
    {
        private VehicleReservation()
        {
        }
        public VehicleReservation(ReservationId reservationId, ParkingSpotId parkingSpotId,BookerName bookerName, string licensePlate,Capacity capacity ,Date reservationDate) : base(reservationId, parkingSpotId,capacity ,reservationDate)
        {
            BookerName = bookerName;
            ChangeLicensePlate(licensePlate);
        }

        public BookerName BookerName { get; private set; }
        public string LicensePlate { get; private set; }

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
