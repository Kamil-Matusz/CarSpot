using CarSpot.Application.Abstractions;
using CarSpot.Core.DomainServices;
using CarSpot.Core.Repositories;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Commands.Handlers
{
    public sealed class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
    {
        private readonly IWeeklyParkingSpotRepository _repository;
        private readonly IParkingReservationService _reservationService;

        public ReserveParkingSpotForCleaningHandler(IWeeklyParkingSpotRepository repository,
            IParkingReservationService reservationService)
        {
            _repository = repository;
            _reservationService = reservationService;
        }

        public async Task HandlerAsync(ReserveParkingSpotForCleaning command)
        {
            var week = new Week(command.date);
            var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();

            _reservationService.ReserveParkingForCleaning(weeklyParkingSpots, new Date(command.date));

            var tasks = weeklyParkingSpots.Select(x => _repository.UpdateAsync(x));
            await Task.WhenAll(tasks);
        }
    }
}
