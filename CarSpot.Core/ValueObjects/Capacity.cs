using CarSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.ValueObjects
{
    public sealed record Capacity
    {
        public int Value { get; }

        public Capacity(int value)
        {
            if(value is < 0 or > 4)
            {
                throw new InvalidCapacityException(value);
            }
            Value = value;
        }

        public static implicit operator int(Capacity value)
           => value.Value;

        public static implicit operator Capacity(int value)
            => new(value);
    }
}
