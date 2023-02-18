using CarSpot.Application.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.Logging.Decorators
{
    internal sealed class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger;

        public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler,
            ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
        {
            _commandHandler = commandHandler;
            _logger = logger;
        }

        public async Task HandlerAsync(TCommand command)
        {
            var commandName = typeof(TCommand).Name;
            _logger.LogInformation("Started handling a command: {CommandName}...", commandName);
            await _commandHandler.HandlerAsync(command);
            _logger.LogInformation("Completed handling a command: {CommandName}.", commandName);
        }
    }
}
