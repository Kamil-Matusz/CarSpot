﻿using CarSpot.Core.Entities;
using CarSpot.Core.Repositories;
using CarSpot.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.DAL.Repositories
{
    internal sealed class PostgresUserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;

        public PostgresUserRepository(CarSpotDbContext dbContext)
        {
            _users = dbContext.Users;
        }

        public Task<User> GetByIdAsync(UserId id)
            => _users.SingleOrDefaultAsync(x => x.UserId == id);

        public Task<User> GetByEmailAsync(Email email)
            => _users.SingleOrDefaultAsync(x => x.Email == email);

        public Task<User> GetByUsernameAsync(Username username)
            => _users.SingleOrDefaultAsync(x => x.Username == username);

        public async Task AddAsync(User user)
            => await _users.AddAsync(user);
    }
}
