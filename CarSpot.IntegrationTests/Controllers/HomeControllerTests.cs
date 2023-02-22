using Shouldly;
using System.Net;
using Xunit;

namespace CarSpot.IntegrationTests.Controllers
{
    public class HomeControllerTests : ControllerTests
    {
        public HomeControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
        {
        }

        [Fact]
        public async Task get_base_endpoint_should_return_404_not_found_status_code_and_api_name()
        {
            var response = await Client.GetAsync("/");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
