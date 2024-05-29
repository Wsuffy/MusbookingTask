using MediatR;
using Microsoft.EntityFrameworkCore;
using Musbooking.Domain.Exceptions;
using Order.Core.Contexts;
using Order.Dal.Repositories;

namespace Order.Dal.SqlLite.Order;

public record DeleteOrderCommand(int OrderId) : IRequest<int>;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IOrderEquipmentRepository _orderEquipmentRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IEquipmentRepository equipmentRepository,
        IOrderEquipmentRepository orderEquipmentRepository)
    {
        _orderRepository = orderRepository;
        _equipmentRepository = equipmentRepository;
        _orderEquipmentRepository = orderEquipmentRepository;
    }

    public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.OrderId <= 0)
                throw new BadRequestExceptionWithLog("Вы попытались ввести некорректный Id", "Incorrect OrderId");

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

            await _orderRepository.DeleteAndSaveAsync(order, cancellationToken);

            var removeRangeInOderEqueipments =
                await _orderEquipmentRepository.GetAllByOrderId(request.OrderId, cancellationToken);

            await _orderEquipmentRepository.DeleteRangeAsync(removeRangeInOderEqueipments, cancellationToken);

            return request.OrderId;
        }
        catch (Exception e)
        {
            throw new BadRequestExceptionWithLog(
                e.Message,
                "Can't find order or equipment(s)");
        }
    }
}