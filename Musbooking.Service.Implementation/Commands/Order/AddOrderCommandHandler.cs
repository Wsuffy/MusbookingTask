﻿using MediatR;
using Musbooking.Application.Models.DTOs.Equipment;
using Musbooking.Application.Models.DTOs.Order;
using Musbooking.Domain.Entities.OrderEquipment;
using Musbooking.Domain.Exceptions;
using Musbooking.Service.Abstractions.Abstractions;

namespace Musbooking.Service.Implementation.Commands.Order;

public record AddOrderCommand(string Description, List<EquipmentDto> Equipments) : IRequest<OrderDto>;

public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderDto>
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IOrderRepository _orderRepository;

    public AddOrderCommandHandler(IEquipmentRepository equipmentRepository, IOrderRepository orderRepository)
    {
        _equipmentRepository = equipmentRepository;
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _orderRepository.BeginTransactionAsync(cancellationToken);
        try
        {
            var order = new Domain.Entities.Order.Order()
            {
                Description = request.Description,
                Equipments = new List<OrderEquipment>()
            };
            decimal price = 0;

            foreach (var equipmentDto in request.Equipments)
            {
                var equipment =
                    await _equipmentRepository.GetByIdAsync(equipmentDto.Id, cancellationToken);

                if (equipment == null || equipment.Amount < equipmentDto.Amount)
                    throw new BadRequestExceptionWithLog(
                        "Вы пытаетесь создать заказ, но не найдет equipment или же его слишком мало на складе",
                        "Invalid equipment or insufficient amount in stock");

                equipment.Amount -= equipmentDto.Amount;

                price += equipment.Price * equipmentDto.Amount;

                var orderEquipment = new OrderEquipment
                {
                    OrderId = order.Id,
                    EquipmentId = equipment.Id,
                    Quantity = equipmentDto.Amount,
                    Order = order
                };

                order.Equipments.Add(orderEquipment);
            }

            order.Price = price;

            await _orderRepository.AddAndSaveAsync(order, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new OrderDto(order);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);

            if (e.GetType() == typeof(BadRequestExceptionWithLog))
                throw new BadRequestExceptionWithLog(
                    "Вы пытаетесь создать заказ, но не найден equipment или же его слишком мало на складе",
                    "Invalid equipment or insufficient amount in stock");

            throw new BadRequestExceptionWithLog("Что-то пошло не так при создании заказа",
                "Something went wrong while creating the order");
        }
    }
}