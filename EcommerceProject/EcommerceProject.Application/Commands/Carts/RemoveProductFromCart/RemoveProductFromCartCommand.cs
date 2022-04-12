﻿using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Carts.RemoveProductFromCart
{
    public class RemoveProductFromCartCommand : ICommand<int>
    {
        public int CartId { get; init; }    
        public int CartProductId { get; init; }
    }
}
