using CarSpot.Api.Entities;
using CarSpot.Core.ValueObject;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Entities
{
    public sealed class CleaningReservation : Reservation
    {
        private CleaningReservation()
        {
        }
        public CleaningReservation(ReservationId id, Date date) : base(id, capacity: 2, date)
        {
        }
    }
}
