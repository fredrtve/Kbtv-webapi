using BjBygg.Application.Common.Interfaces;
using System;
using System.Linq;

namespace BjBygg.Infrastructure.Services
{
    public class IdGenerator : IIdGenerator
    {
        private Random random = new Random();

        public string Generate()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 7)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
