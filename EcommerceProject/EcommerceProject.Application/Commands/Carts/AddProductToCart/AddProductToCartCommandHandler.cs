﻿using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Carts.AddProductToCart
{
    public class AddProductToCartCommandHandler : ICommandHandler<AddProductToCartCommand, int>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public AddProductToCartCommandHandler(ICartRepository cartRepository, IUserRepository userRepository,
                                IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<CommandResult<int>> Handle(AddProductToCartCommand command, CancellationToken cancellationToken)
        {
            var spec = new SpecificationBase<Cart>(x => x.UserId == command.UserId);
            var cart = await _cartRepository.FindOneAsync(spec, cancellationToken);
            if (cart == null) return CommandResult<int>.Error("Do not find a cart with customer id.");
           
            var product = await _productRepository.FindOneAsync(command.ProductId, cancellationToken);
            if (product == null) return CommandResult<int>.Error("Your product is not exist.");

            var cartProduct = new CartProduct(command.ProductId, command.Quantity, product.Price);
            cart.AddCartProduct(cartProduct);
            await _cartRepository.SaveAsync(cart, cancellationToken);

            return CommandResult<int>.Success(product.Id);
        }
    }
}
