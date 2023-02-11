using CarSpot.Api.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Exceptions
{
    public sealed class InvalidBookerNameException : CustomException
    {
        public InvalidBookerNameException() : base("Booker name is invalid.")
        {
        }
    }
}
