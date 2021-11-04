using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Common
{
    public static class QueryableExtensions
    {
        public static async Task<int> AddressCountAsync<T>(this IQueryable<T> query, string address) where T : IAddress
        {
            return await query.Where(x => EF.Functions.Like(x.Address, $"{address}%")).CountAsync();
        }
    }
}
