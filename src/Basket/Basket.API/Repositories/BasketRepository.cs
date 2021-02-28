﻿using Basket.API.Data.Interfaces;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _basketContext;

        public BasketRepository(IBasketContext basketContext)
        {
            _basketContext = basketContext ?? throw new ArgumentNullException(nameof(basketContext));
        }

        public async Task<BasketCart> GetBasket(string userName)
        {
            var basket = await _basketContext
                                .Redis
                                .StringGetAsync(userName);

            if (basket.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<BasketCart>(basket); //because redis returns us the json and we have to deserlialze it into the class
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            return await _basketContext
                           .Redis
                           .KeyDeleteAsync(userName);
        }

        public async Task<BasketCart> UpdateBasket(BasketCart basket)
        {
            var updated = await _basketContext
                              .Redis
                              .StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            if (!updated)
            {
                return null;
            }
            return await GetBasket(basket.UserName);
        }
    }
}
