﻿using Basket.API.Data.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Data
{
    public class BasketContext : IBasketContext
    {
        private readonly ConnectionMultiplexer _redisConnection;
        public IDatabase Redis { get; }

        public BasketContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));
            Redis = _redisConnection.GetDatabase();
        }

    }
}
