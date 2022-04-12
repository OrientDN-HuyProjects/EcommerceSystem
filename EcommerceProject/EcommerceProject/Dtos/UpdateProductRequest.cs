﻿using EcommerceProject.Domain.SharedKermel;

namespace EcommerceProject.API.Dtos
{
    public class UpdateProductRequest
    {
        public string Name { get; init; }
        public MoneyValue Price { get; init; }
        public string TradeMark { get; init; }
        public string Origin { get; init; }
        public string Discription { get; init; }
    }
}
