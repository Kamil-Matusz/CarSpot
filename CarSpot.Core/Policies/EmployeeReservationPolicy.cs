using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Policies
{
    internal sealed class EmployeeReservationPolicy : IReservationPolicy
    {
        public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.Employee;

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, BookerName bookerName)
        {
            var totalReservations = weeklyParkingSpots
                .SelectMany(x => x.Reservations)
                .Count(x => x.BookerName == bookerName);

            return totalReservations < 2;
        }
    }
}
