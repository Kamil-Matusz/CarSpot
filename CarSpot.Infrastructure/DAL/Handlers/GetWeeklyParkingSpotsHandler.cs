using CarSpot.Application.Abstractions;
using CarSpot.Application.DTO;
using CarSpot.Application.Queries;
using CarSpot.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL.Handlers
{
    internal sealed class GetWeeklyParkingSpotsHandler : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
    {
        private readonly CarSpotDbContext _dbContext;

        public GetWeeklyParkingSpotsHandler(CarSpotDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<IEnumerable<WeeklyParkingSpotDto>> HandleAsync(GetWeeklyParkingSpots query)
        {
            var week = query.Date.HasValue ? new Week(query.Date.Value) : null;
            var weeklyParkingSpots = await _dbContext.WeeklyParkingSpots
                .Where(x => week == null || x.Week == week)
                .Include(x => x.Reservations)
                .AsNoTracking()
                .ToListAsync();

            return weeklyParkingSpots.Select(x => x.AsDto());
        }
    }
}
