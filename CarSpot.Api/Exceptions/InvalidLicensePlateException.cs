namespace CarSpot.Api.Exceptions
{
    public sealed class InvalidLicencePlateException : CustomException
    {
        public string LicencePlate { get; }

        public InvalidLicencePlateException(string licencePlate)
            : base("Licence plate is invalid.")
        {
            LicencePlate = licencePlate;
        }
    }
}
