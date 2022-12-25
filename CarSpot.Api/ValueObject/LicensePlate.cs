using CarSpot.Api.Exceptions;

namespace CarSpot.Api.ValueObject
{
    public record LicensePlate : IEquatable<LicensePlate>
    {
        // license plate name
        public string Value { get; }

        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyLicensePlateException();
            }

            if (value.Length is < 5 or > 8)
            {
                throw new InvalidLicensePlateException();
            }
            Value = value;
        }

        // przeciążanie z/na typu LicensePlate
        public static implicit operator string(LicensePlate licensePlate) => licensePlate?.Value;
        public static implicit operator LicensePlate(string licensePlate) => new LicensePlate(licensePlate);
    }
}
