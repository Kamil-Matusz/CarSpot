using CarSpot.Api.Commands;
using CarSpot.Api.Entities;
using CarSpot.Application.Abstractions;
using CarSpot.Application.Exceptions;
using CarSpot.Core.Entities;
using CarSpot.Core.Repositories;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Commands.Handlers
{
    public sealed class ChangeReservationLicencePlateHandler : ICommandHandler<ChangeReservationLicencePlate>
    {
        private readonly IWeeklyParkingSpotRepository _repository;

        public ChangeReservationLicencePlateHandler(IWeeklyParkingSpotRepository repository)
            => _repository = repository;

        public async Task HandlerAsync(ChangeReservationLicencePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservation(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                throw new WeeklyParkingSpotNotFoundException();
            }

            var reservationId = new ReservationId(command.ReservationId);
            var reservation = weeklyParkingSpot.Reservations
                .OfType<VehicleReservation>()
                .SingleOrDefault(x => x.ReservationId == reservationId);

            if (reservation is null)
            {
                throw new ReservationNotFoundException(command.ReservationId);
            }

            reservation.ChangeLicensePlate(command.LicencePlate);
            await _repository.UpdateAsync(weeklyParkingSpot);
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservation(ReservationId id)
            => (await _repository.GetAllWeeklyAsync())
                .SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == id));
    }
}
