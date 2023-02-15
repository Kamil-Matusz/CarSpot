using CarSpot.Api.Commands;
using CarSpot.Api.Entities;
using CarSpot.Application.Abstractions;
using CarSpot.Application.Exceptions;
using CarSpot.Core.Repositories;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Commands.Handlers
{
    public sealed class DeleteReservationHandler : ICommandHandler<DeleteReservation>
    {
        private readonly IWeeklyParkingSpotRepository _repository;

        public DeleteReservationHandler(IWeeklyParkingSpotRepository repository)
            => _repository = repository;

        public async Task HandlerAsync(DeleteReservation command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservation(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                throw new WeeklyParkingSpotNotFoundException();
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            await _repository.UpdateAsync(weeklyParkingSpot);
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservation(ReservationId id)
            => (await _repository.GetAllWeeklyAsync())
                .SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == id));
    }
}
