﻿using CarSpot.Api.Entities;

namespace CarSpot.Api.Services
{
    public class ReservationsService
    {
        private static readonly List<Reservation> _reservations = new();
        private static int _id = 1;
        private static readonly List<string> _parkingSpotNames = new()
        {
            "P1",
            "P2",
            "P3",
        };

        public Reservation Get(int id)
        {
            var reservation = _reservations.SingleOrDefault(x => x.ReservationId == id);
            if (reservation is null)
            {
                return null;
            }

            return reservation;
        }

        public IEnumerable<Reservation> GetAll() 
        {
            return _reservations;
        }

        public int? Create(Reservation reservation)
        {   
            var now = DateTime.UtcNow.Date;
            var pastDays = now.DayOfWeek is DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
            var remainingDays = 7 - pastDays;

            if (_parkingSpotNames.All(x => x != reservation.ParkingSpotName))
            {
                return default;
            }

            if(! (reservation.ReservationDate.Date >= now &&  reservation.ReservationDate.Date <= now.AddDays(remainingDays)))
            {
                return default;
            }

            if(string.IsNullOrWhiteSpace(reservation.LicensePlate)) 
            {
                return default;
            }

            var reservationAlreadyExists = _reservations.Any(x => x.ParkingSpotName == reservation.ParkingSpotName && x.ReservationDate.Date == reservation.ReservationDate.Date);

            if (reservationAlreadyExists)
            {
                return default;
            }
            reservation.ReservationId = _id;
            _id++;

            _reservations.Add(reservation);
            return reservation.ReservationId;
        }

        public bool Update(Reservation reservation)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.ReservationId == reservation.ReservationId);
            if (existingReservation is null)
            {
                return false;
            }

            if(existingReservation.ReservationDate <= DateTime.UtcNow)
            {
                return false;
            }

            existingReservation.LicensePlate = reservation.LicensePlate;
            return true;
        }

        public bool Delete(int id)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.ReservationId == id);
            if (existingReservation is null)
            {
                return false;
            }

            _reservations.Remove(existingReservation);
            return true;
        }
    }
}
