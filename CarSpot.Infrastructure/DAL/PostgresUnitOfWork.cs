using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL
{
    internal sealed class PostgresUnitOfWork : IUnitOfWork
    {
        private readonly CarSpotDbContext _dbContext;

        // transaction in database
        public PostgresUnitOfWork(CarSpotDbContext dbContext)
            => _dbContext = dbContext;
        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await action();
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
