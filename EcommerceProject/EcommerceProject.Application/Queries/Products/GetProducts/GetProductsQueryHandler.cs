﻿using EcommerceProject.Domain.AggregatesModel.ProductAggregate;
using EcommerceProject.Infrastructure.CQRS.Queries;

namespace EcommerceProject.Application.Queries.Products.GetProducts
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var productRepo = await _productRepository.FindAllAsync(null, cancellationToken);
            var products = productRepo.ToList();

            var result = from product in products
                         where query.Name is null || product.Name == query.Name
                         where query.TradeMark is null || product.TradeMark == query.TradeMark
                         where query.Origin is null || product.Origin == query.Origin
                         where query.MaxValue is null || product.Price.Value <= query.MaxValue.Value
                         where query.MinValue is null || product.Price.Value >= query.MinValue.Value
                         select product;

            return result;
        }
    }
}