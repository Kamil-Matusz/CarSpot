using CarSpot.Api.Exceptions;

namespace CarSpot.Api.ValueObject
{
    public sealed record BookerName(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidBookerNameException();

        public static implicit operator string(BookerName name)
            => name.Value;

        public static implicit operator BookerName(string value)
            => new(value);
    }
}
