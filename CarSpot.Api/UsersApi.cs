using CarSpot.Application.Abstractions;
using CarSpot.Application.Commands;
using CarSpot.Application.DTO;
using CarSpot.Application.Queries;
using CarSpot.Infrastructure;
using Microsoft.Extensions.Options;

namespace CarSpot.Api
{
    internal static class UsersApi
    {
        public static WebApplication UseUsersApi(this WebApplication app)
        {
            app.MapGet("api", (HttpContext context, IOptions<AppOptions> options) => {
                Results.Ok(options.Value.Name);
            });

            app.MapGet("api/users/{userId:guid}", async (Guid userId, IQueryHandler<GetUser, UserDto> handler) =>
            {
                var userDto = await handler.HandleAsync(new GetUser { UserId = userId });
                return userDto is null ? Results.NotFound() : Results.Ok(userDto);
            }).RequireAuthorization("is-admin");

            app.MapPost("api/users", async (SignUp command, ICommandHandler<SignUp> handler) =>
            {
                command = command with { UserId = Guid.NewGuid() };
                await handler.HandlerAsync(command);
                return Results.Ok(command);
            });

            return app;
        }
    }
}
