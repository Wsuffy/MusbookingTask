using MediatR;
using Musbooking.Application.Models.DTOs;
using Musbooking.Application.Models.DTOs.Order;
using Musbooking.Domain.Exceptions;
using Order.Dal.Repositories;

namespace Order.Dal.SqlLite.Order;

public record GetOrderByIdQuery(int PageSize, int PageNumber) : IRequest<PagedResultDto<OrderDto>>;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, PagedResultDto<OrderDto>>
{
    private readonly IOrderReadRepository _orderReadRepository;

    public GetOrderByIdQueryHandler(IOrderReadRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
    }

    public async Task<PagedResultDto<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.PageSize <= 0 && request.PageNumber <= 0)
                throw new BadRequestExceptionWithLog("Неправильный размер или количество старниц",
                    "Неправильный размер или количество старниц");

            var orders =
                await _orderReadRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

            var ordersDto = orders.Select(order => new OrderDto(order)).OrderBy(x => x.CreatedAt).ToList();

            return new PagedResultDto<OrderDto>()
            {
                Items = ordersDto,
                Count = ordersDto.Count
            };
        }
        catch (Exception e)
        {
            throw new BadRequestExceptionWithLog(e.Message, e.Message);
        }
    }
}