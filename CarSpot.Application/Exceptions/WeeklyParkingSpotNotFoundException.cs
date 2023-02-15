using CarSpot.Api.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Exceptions
{
    public sealed class WeeklyParkingSpotNotFoundException : CustomException
    {
        public Guid? Id { get; }

        public WeeklyParkingSpotNotFoundException()
            : base("Weekly parking spot with ID was not found.")
        {
        }

        public WeeklyParkingSpotNotFoundException(Guid id)
            : base($"Weekly parking spot with ID: {id} was not found.")
        {
            Id = id;
        }
    }
}
