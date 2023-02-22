using CarSpot.Api.Services;
using CarSpot.Application.Commands;
using CarSpot.Application.DTO;
using CarSpot.Core.Entities;
using CarSpot.Core.Repositories;
using CarSpot.Core.ValueObjects;
using CarSpot.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using System.Net;
using Xunit;

namespace CarSpot.IntegrationTests.Controllers
{
    public class UsersControllerTests : ControllerTests, IDisposable
    {
        private readonly TestDatabase _testDatabase;
        private IUserRepository _userRepository;

        public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
        {
            _testDatabase = new TestDatabase();
        }

        public void Dispose()
        {
            _testDatabase?.Dispose();
        }

        [Fact]
        public async Task post_users_should_return_no_content_status_code()
        {
            var command = new SignUp(Guid.Empty, "test-user@onet.pl", "test-user", "secret","Test Doe", "user");
            var response = await Client.PostAsJsonAsync("users", command);

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task post_sign_in_should_return_ok_200_status_code_and_jwt()
        {
            var passwordManager = new PasswordManager(new PasswordHasher<User>());
            var clock = new Clock();
            const string password = "secret";

            var user = new User(Guid.NewGuid(), "test-user1@myspot.io","test-user1", passwordManager.Secure(password), "Test Doe", Role.User(), clock.CurrentDate());

            await _testDatabase.DbContext.Users.AddAsync(user);
            await _testDatabase.DbContext.SaveChangesAsync();

            var command = new SignIn(user.Email, password);
            var response = await Client.PostAsJsonAsync("users/sign-in", command);
            var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();

            jwt.ShouldNotBeNull();
            jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();

        }
    }
}
