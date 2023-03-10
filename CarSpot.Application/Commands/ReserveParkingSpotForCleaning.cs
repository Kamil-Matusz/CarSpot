using CarSpot.Application.Abstractions;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Commands
{
    public record ReserveParkingSpotForCleaning(DateTime date) : ICommand;
}
