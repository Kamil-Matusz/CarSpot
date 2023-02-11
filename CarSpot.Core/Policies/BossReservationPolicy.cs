using CarSpot.Api.Entities;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Policies
{
    internal sealed class BossReservationPolicy : IReservationPolicy
    {
        public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.Boss;

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, BookerName bookerName) => true;
    }
}
