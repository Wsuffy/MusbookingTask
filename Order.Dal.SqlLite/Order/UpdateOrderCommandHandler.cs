﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Musbooking.Application.Models.DTOs.Equipment;
using Musbooking.Application.Models.DTOs.Order;
using Musbooking.Domain.Exceptions;
using Order.Core.Contexts;
using Order.Core.Entities.OrderEquipment;
using Order.Dal.Repositories;

namespace Order.Dal.SqlLite.Order;

public record UpdateOrderCommand(int OrderId, string Description, List<EquipmentDto> Equipments) : IRequest<OrderDto>;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEquipmentRepository _equipmentRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IEquipmentRepository equipmentRepository)
    {
        _orderRepository = orderRepository;
        _equipmentRepository = equipmentRepository;
    }

    public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.OrderId <= 0)
                throw new BadRequestExceptionWithLog("Вы попытались ввести некорректный Id", "Incorrect orderId");
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

            if (order == null)
                throw new BadRequestExceptionWithLog("Заказ с таким Id не найден", "Order not found");

            foreach (var orderEquipment in order.Equipments)
            {
                var equipment =
                    await _equipmentRepository.GetByIdAsync(orderEquipment.EquipmentId, cancellationToken);

                if (equipment == null)
                    throw new BadRequestExceptionWithLog("Указанный Equipment в заказе не найден",
                        "Can't find equipment in remove command");

                equipment.Amount += orderEquipment.Quantity;
            }

            order.Equipments.Clear();

            await _orderRepository.SaveChangesAsync(cancellationToken);

            order.Description = request.Description;
            order.Price = 0;
            order.UpdatedAt = DateTime.Now;

            foreach (var orderEquipment in request.Equipments)
            {
                var equipment =
                    await _equipmentRepository.GetByIdAsync(orderEquipment.Id, cancellationToken);

                if (equipment == null || equipment.Amount < orderEquipment.Amount)
                    throw new BadRequestExceptionWithLog(
                        "Вы пытаетесь создать заказ, но не найдет equipment или же его слишком мало на складе",
                        "Invalid equipment or insufficient amount in stock");

                equipment.Amount -= orderEquipment.Amount;
                order.Price += equipment.Price * orderEquipment.Amount;

                var orderEquipmentToDb = new OrderEquipment
                {
                    OrderId = order.Id,
                    EquipmentId = equipment.Id,
                    Quantity = orderEquipment.Amount,
                };

                order.Equipments.Add(orderEquipmentToDb);
            }

            await _orderRepository.SaveChangesAsync(cancellationToken);

            return new OrderDto(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new BadRequestExceptionWithLog(
                e.Message,
                e.Message);
        }
    }
}