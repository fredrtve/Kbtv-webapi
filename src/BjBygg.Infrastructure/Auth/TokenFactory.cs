﻿using BjBygg.Application.Identity.Common.Interfaces;
using System;
using System.Security.Cryptography;


namespace BjBygg.Infrastructure.Auth
{
    public class TokenFactory
    {
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
