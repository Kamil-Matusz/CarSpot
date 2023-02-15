using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Abstractions
{
    public interface ICommandHandler<in TCommand> where TCommand : class,ICommand
    {
        Task HandlerAsync(TCommand command);
    }
}
