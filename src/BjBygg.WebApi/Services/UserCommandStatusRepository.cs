using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Core.Entities;
using BjBygg.Infrastructure.Data;
using BjBygg.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Services
{
    public class UserCommandStatusRepository
    {
        private DbContextOptions<AppDbContext> _dbOptions;
        private CurrentUserService _currentUserService;
        private SyncTimestamps _syncTimestamps;
        private Dictionary<string, UserCommandStatus> _userCommandStatuses;
        public UserCommandStatusRepository(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
            _currentUserService = new CurrentUserService("SYSTEM", Roles.Admin);
            _syncTimestamps = new SyncTimestamps();
        }

        public UserCommandStatus GetStatusOrDefault(string userName)
        {
            return _userCommandStatuses.GetValueOrDefault(userName); 
        }


        public async Task SetStatusAsync(UserCommandStatus status)
        {
            var isExisting = _userCommandStatuses.ContainsKey(status.UserName);
            if (isExisting) _userCommandStatuses[status.UserName] = status;
            else _userCommandStatuses.Add(status.UserName, status);

            using (var context = new AppDbContext(_dbOptions, _currentUserService, _syncTimestamps))
            {
                if (isExisting) context.Update(status);
                else context.Add(status);
                await context.SaveChangesAsync();
            };
        }

        public void FetchFromDb()
        {
            using (var context = new AppDbContext(_dbOptions, _currentUserService, _syncTimestamps))
            {
                _userCommandStatuses = context.UserCommandStatuses.ToDictionary(x => x.UserName);
            };
        }
    }
}
