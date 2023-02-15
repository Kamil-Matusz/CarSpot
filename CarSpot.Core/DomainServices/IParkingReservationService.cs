using CarSpot.Api.Entities;
using CarSpot.Core.Entities;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.DomainServices
{
    public interface IParkingReservationService
    {
        void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> parkingSpots, JobTitle jobTitle,WeeklyParkingSpot parkingSpotToReserve, VehicleReservation reservation);
        void ReserveParkingForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpots,Date date);
    }
}
