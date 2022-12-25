using CarSpot.Api.Exceptions;

namespace CarSpot.Api.ValueObject
{
    public sealed record ParkingSpotName(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidParkingSpotNameException();

        public static implicit operator string(ParkingSpotName name)
            => name.Value;

        public static implicit operator ParkingSpotName(string value)
            => new(value);
    }
}
