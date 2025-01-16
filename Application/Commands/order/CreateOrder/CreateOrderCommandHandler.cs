﻿using Application.Dtos.Order;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.order.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrdersRepository ordersRepository, IMapper mapper, IProductsRepository productsRepository)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
        _productsRepository = productsRepository;
    }
    
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var products = await _productsRepository.GetProductByIdsAsync(request.Items.Select(i => i.ProductId));
        var items = new List<OrderItems>();
        foreach (var itemDto in request.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == itemDto.ProductId);
            if (product == null)
                throw new ArgumentException($"Product with ID {itemDto.ProductId} not found.");

            var orderItem = OrderItems.Create(itemDto.Quantity, product);
            items.Add(orderItem);
        }
        var order = Orders.Create(request.CustomerId, items);


        await _ordersRepository.AddOrderAsync(order, cancellationToken);


        return order.Id;
    }
}