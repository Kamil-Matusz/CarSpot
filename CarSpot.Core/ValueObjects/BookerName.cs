using CarSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.ValueObjects
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
