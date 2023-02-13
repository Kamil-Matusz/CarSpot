using CarSpot.Api.Entities;
using CarSpot.Api.Services;
using CarSpot.Core.Entities;
using CarSpot.Core.Exceptions;
using CarSpot.Core.Policies;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.DomainServices
{
    public sealed class ParkingReservationService : IParkingReservationService
    {
        private readonly IEnumerable<IReservationPolicy> _policies;
        private readonly IClock _clock;

        public ParkingReservationService(IEnumerable<IReservationPolicy> policies, IClock clock)
        {
            _policies = policies;
            _clock = clock;
        }

        public void ReserveParkingForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpots, Date date)
        {
            foreach (var parkingSpot in allParkingSpots)
            {
                var reservationsForSameDate = parkingSpot.Reservations.Where(x => x.ReservationDate == date);
                parkingSpot.RemoveReservations(reservationsForSameDate);

                var cleaningReservation = new CleaningReservation(ReservationId.Create(),parkingSpot.WeeklyParkingSpotId,date);
                parkingSpot.AddReservation(cleaningReservation, new Date(_clock.CurrentDate()));
            }
        }

        public void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> parkingSpots, JobTitle jobTitle, WeeklyParkingSpot parkingSpotToReserve, VehicleReservation reservation)
        {
            var parkingSpotId = parkingSpotToReserve.WeeklyParkingSpotId;
            var policy = _policies.SingleOrDefault(x => x.CanBeApplied(jobTitle));

            if(policy is null)
            {
                throw new NoReservationPolicyFoundException(jobTitle);
            }

            if(!policy.CanReserve(parkingSpots,reservation.BookerName))
            {
                throw new CannotReserveParkingSpotException(parkingSpotId);
            }

            parkingSpotToReserve.AddReservation(reservation, new Date(_clock.CurrentDate()));
        }
    }
}
