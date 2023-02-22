using CarSpot.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CarSpot.IntegrationTests
{
    internal class TestDatabase : IDisposable
    {
        public CarSpotDbContext DbContext { get; }
        public TestDatabase()
        {
            var options = new OptionsProvider().Get<PostgresOptions>("postgres");
            DbContext = new CarSpotDbContext(new DbContextOptionsBuilder<CarSpotDbContext>().UseNpgsql(options.ConnectionString).Options);
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }
}
