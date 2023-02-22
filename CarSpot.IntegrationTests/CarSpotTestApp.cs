using Microsoft.AspNetCore.Mvc.Testing;
using CarSpot;

namespace CarSpot.IntegrationTests
{
    internal sealed class CarSpotTestApp : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }

        public CarSpotTestApp(Action<IServiceCollection> services)
        {
            Client = WithWebHostBuilder(builder =>
            {
                if(services is not null)
                {
                    builder.ConfigureServices(services);
                }
                builder.UseEnvironment("test");
            }).CreateClient();
        }

    }
}
