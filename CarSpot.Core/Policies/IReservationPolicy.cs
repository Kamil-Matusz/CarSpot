using CarSpot.Api.Entities;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Policies
{
    public interface IReservationPolicy
    {
        bool CanBeApplied(JobTitle jobTitle);
        bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, BookerName bookerName);
    }
}
