using CarSpot.Api.Commands;
using CarSpot.Api.Services;
using CarSpot.Application.Abstractions;
using CarSpot.Application.Exceptions;
using CarSpot.Core.DomainServices;
using CarSpot.Core.Entities;
using CarSpot.Core.Repositories;
using CarSpot.Core.ValueObject;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Commands.Handlers
{
    public sealed class ReserveParkingSpotForVehicleHandler : ICommandHandler<ReserveParkingSpotForVehicle>
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IParkingReservationService _parkingReservationService;

        public ReserveParkingSpotForVehicleHandler(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IParkingReservationService parkingReservationService)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _parkingReservationService = parkingReservationService;
        }

        public async Task HandlerAsync(ReserveParkingSpotForVehicle command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var week = new Week(_clock.CurrentDate());
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetByWeekAsync(week);
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.WeeklyParkingSpotId == parkingSpotId);
            if (parkingSpotToReserve is null)
            {
                throw new WeeklyParkingSpotNotFoundException(parkingSpotId);
            }

            var reservation = new VehicleReservation(command.ReservationId, command.ParkingSpotId, command.BookerName, command.LicensePlate, command.Capacity, new Date(command.ReservationDate));

            _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee, parkingSpotToReserve, reservation);

            await _weeklyParkingSpotRepository.UpdateAsync(parkingSpotToReserve);
        }
    }
}
