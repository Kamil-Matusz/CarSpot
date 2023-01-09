using CarSpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.ValueObject
{
    public sealed record ParkingSpotId
    {
        public Guid Value { get; }

        public ParkingSpotId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidEntityIdException(value);
            }

            Value = value;
        }

        public static ParkingSpotId Create() => new(Guid.NewGuid());

        public static implicit operator Guid(ParkingSpotId date)
            => date.Value;

        public static implicit operator ParkingSpotId(Guid value)
            => new(value);

        public override string ToString() => Value.ToString("N");
    }
}
