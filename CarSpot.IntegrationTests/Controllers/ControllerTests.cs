using CarSpot.Infrastructure.DAL;
using Xunit;

namespace CarSpot.IntegrationTests.Controllers
{
    [Collection("api")]
    public abstract class ControllerTests : IClassFixture<OptionsProvider>
    {
        protected HttpClient Client { get; }

        public ControllerTests(OptionsProvider optionsProvider)
        {
            var app = new CarSpotTestApp(ConfigureServices);
            Client = app.Client;
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
