using CarSpot.Api.Exceptions;
using CarSpot.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Exceptions
{
    public sealed class ParkingSpotCapacityExceededException : CustomException
    {
        public ParkingSpotId ParkingSpotId { get; }

        public ParkingSpotCapacityExceededException(ParkingSpotId parkingSpotId)
            : base($"Parking spot with ID: {parkingSpotId} exceeded its reservation capacity.")
        {
            ParkingSpotId = parkingSpotId;
        }
    }
}
