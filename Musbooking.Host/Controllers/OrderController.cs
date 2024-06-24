using MediatR;
using Microsoft.AspNetCore.Mvc;
using Musbooking.Application.Models.DTOs.Equipment;
using Musbooking.Application.Models.Requests.Equipment;
using Musbooking.Application.Models.Requests.Order;
using Musbooking.Service.Implementation.Commands.Equipment;
using Musbooking.Service.Implementation.Commands.Order;

namespace Musbooking.Host.Controllers;

[Route("api")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("equipments")]
    public async Task<IActionResult> CreateEquipmentAsync(EquipmentRequest request,
        CancellationToken cancellationToken)
    {
        var entity = await _mediator.Send(new AddEquipmentCommand(request.Name, request.Amount, request.Price),
            cancellationToken);
        return Ok(entity);
    }

    [HttpPost]
    [Route("orders")]
    public async Task<IActionResult> CreateOrderAsync(OrderRequest request, CancellationToken cancellationToken)
    {
        var equipments = request.EquipmentInOrders.Select(equipmentInOrder =>
            new EquipmentDto() { Id = equipmentInOrder.Id, Amount = equipmentInOrder.Quantity }).ToList();

        var entity = await _mediator.Send(new AddOrderCommand(request.Description, equipments), cancellationToken);

        return Ok(entity);
    }

    [HttpDelete]
    [Route("orders/{orderId:int}")]
    public async Task<IActionResult> DeleteOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteOrderCommand(orderId), cancellationToken));
    }

    [HttpPatch]
    [Route("orders/{orderId:int}")]
    public async Task<IActionResult> UpdateOrderAsync(int orderId, OrderRequest request,
        CancellationToken cancellationToken)
    {
        var equipments = request.EquipmentInOrders.Select(equipmentInOrder =>
            new EquipmentDto() { Id = equipmentInOrder.Id, Amount = equipmentInOrder.Quantity }).ToList();


        var entity = await _mediator.Send(new UpdateOrderCommand(orderId, request.Description, equipments),
            cancellationToken);


        return Ok(entity);
    }

    [HttpGet]
    [Route("orders/get{pageSize:int}&{pageNumber:int}")]
    public async Task<IActionResult> GetOrdersWithPaginationAsync(int pageSize, int pageNumber,
        CancellationToken cancellationToken)
    {
        return Ok(_mediator.Send(new GetOrderByIdQuery(pageSize, pageNumber), cancellationToken));
    }
}