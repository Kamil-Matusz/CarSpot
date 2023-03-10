namespace CarSpot.Api.Exceptions
{
    public sealed class InvalidReservationDateException : CustomException
    {
        public DateTime Date { get; }

        public InvalidReservationDateException(DateTime date)
            : base($"Reservation date is invalid.")
        {
            Date = date;
        }
    }
}
