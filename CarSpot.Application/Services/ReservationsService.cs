using CarSpot.Api.Commands;
using CarSpot.Api.DTO;
using CarSpot.Api.Entities;
using CarSpot.Application.Commands;
using CarSpot.Core.DomainServices;
using CarSpot.Core.Entities;
using CarSpot.Core.Repositories;
using CarSpot.Core.ValueObject;
using CarSpot.Core.ValueObjects;
using System.Net.Http.Headers;

namespace CarSpot.Api.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IParkingReservationService _parkingReservationService;
        public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository,IParkingReservationService parkingReservationService)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _parkingReservationService = parkingReservationService;
        }

        /*public ReservationDto Get(Guid id)
        {
            return GetAllWeekly().SingleOrDefault(x => x.ReservationDtoId== id);
        }

        //public IEnumerable<Reservation> GetAll() 
        //{
        //    return _reservations;
        //}

        public IEnumerable<ReservationDto> GetAllWeekly() => _weeklyParkingSpotRepository.GetAllWeekly().SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto
            {
                ReservationDtoId = x.ReservationId,
                ParkingSpotId = x.ParkingSpotId,
                BookerName = x.BookerName,
                ReservationDate = x.ReservationDate
            });
        

        public Guid? Create(CreateReservation command)
        {   
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = _weeklyParkingSpotRepository.Get(parkingSpotId);
            if(weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.BookerName, command.LicensePlate, command.ReservationDate);
            weeklyParkingSpot.AddReservation(reservation,_clock.CurrentDate());
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
            return reservation.ReservationId;
        }

        public bool Update(ChangeReservationLicencePlate command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if(weeklyParkingSpot is null)
            {
                return false;
            }

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.ReservationId == command.ReservationId);
            if (existingReservation is null)
            {
                return false;
            }

            if(existingReservation.ReservationDate <= _clock.CurrentDate())
            {
                return false;
            }

            existingReservation.ChangeLicensePlate(command.LicencePlate);
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
            return true;
        }

        public bool Delete(DeleteReservation command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if(weeklyParkingSpot is null)
            {
                return false;
            }
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.ReservationId == command.ReservationId);
            if (existingReservation is null)
            {
                return false;
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId) => _weeklyParkingSpotRepository.GetAllWeekly().SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));
    }*/

        public async Task<ReservationDto> GetAsync(Guid id)
        {
            var reservations = await GetAllWeeklyAsync();
            return reservations.SingleOrDefault(x => x.ReservationDtoId == id);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync()
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllWeeklyAsync();
            
            return weeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto
            {
                ReservationDtoId = x.ReservationId,
                ParkingSpotId = x.ParkingSpotId,
                BookerName = x is VehicleReservation vr ? vr.BookerName : String.Empty,
                ReservationDate = x.ReservationDate.Value.Date
            });
        }


        public async Task<Guid?> CreateAsync(ReserveParkingSpotForVehicle command)
        {
            /*var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = await _weeklyParkingSpotRepository.GetAsync(parkingSpotId);
            if (weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.BookerName, command.LicensePlate, command.ReservationDate);
            weeklyParkingSpot.AddReservation(reservation, _clock.CurrentDate());
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
            return reservation.ReservationId;*/

            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var week = new Week(_clock.CurrentDate());
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetByWeekAsync(week);
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.WeeklyParkingSpotId == parkingSpotId);
            if (parkingSpotToReserve is null)
            {
                return default;
            }

            var reservation = new VehicleReservation(command.ReservationId, command.ParkingSpotId, command.BookerName, command.LicensePlate, new Date(command.ReservationDate));
            
            _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee, parkingSpotToReserve, reservation);

            await _weeklyParkingSpotRepository.UpdateAsync(parkingSpotToReserve);
            return reservation.ReservationId;
        }

        public async Task<bool> UpdateAsync(ChangeReservationLicencePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                return false;
            }

            //var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.ReservationId == command.ReservationId);
            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations
                .OfType<VehicleReservation>()
                .SingleOrDefault(x => x.ReservationId == reservationId);
            if (existingReservation is null)
            {
                return false;
            }

            if (existingReservation.ReservationDate.Value.Date <= _clock.CurrentDate())
            {
                return false;
            }

            existingReservation.ChangeLicensePlate(command.LicencePlate);
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
            return true;
        }

        public async Task<bool> DeleteAsync(DeleteReservation command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                return false;
            }
            // var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.ReservationId == command.ReservationId);
            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.ReservationId == reservationId);
            if (existingReservation is null)
            {
                return false;
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
            return true;
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId reservationId)
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllWeeklyAsync();
            return weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));
        }

        public async Task ReserveForCleaningAsync(ReserveParkingSpotForCleaning command)
        {
            var week = new Week(command.date);
            var weeklyParkingSpots = (await _weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();

            _parkingReservationService.ReserveParkingForCleaning(weeklyParkingSpots, new Date(command.date));

           /* var tasks = weeklyParkingSpots.Select(x => _weeklyParkingSpotRepository.UpdateAsync(x));
            await Task.WhenAll(tasks);*/

            foreach(var parkingSpot in weeklyParkingSpots)
            {
                await _weeklyParkingSpotRepository.UpdateAsync(parkingSpot);
            }
        }
    }
}
