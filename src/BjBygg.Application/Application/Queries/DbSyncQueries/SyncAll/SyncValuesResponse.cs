using BjBygg.Application.Application.Common.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncValuesResponse
    {
        public ApplicationUserDto? CurrentUser { get; set; }

        public LeaderSettingsDto? LeaderSettings { get; set; }
    }
}
