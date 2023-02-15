using CarSpot.Api.Entities;
using CarSpot.Core.Entities;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Policies
{
    internal sealed class ManagerReservationPolicy : IReservationPolicy
    {
        public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.Manager;

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, BookerName bookerName)
        {
            var totalReservations = weeklyParkingSpots
                .SelectMany(x => x.Reservations)
                .OfType<VehicleReservation>()
                .Count(x => x.BookerName == bookerName);

            return totalReservations <= 4;
        }
    }
}
