using CarSpot.Api.Exceptions;
using CarSpot.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Exceptions
{
    public sealed class CannotReserveParkingSpotException : CustomException
    {
        public ParkingSpotId ParkingSpotId { get;}
        public CannotReserveParkingSpotException(ParkingSpotId parkingSpotId) : base($"Cannot reserve parking spot with ID: {parkingSpotId}")
        {
            ParkingSpotId = parkingSpotId;
        }
    }
}
