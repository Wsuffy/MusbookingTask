namespace Musbooking.Application.Models.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public decimal Price { get; set; }

    public OrderDto(global::Musbooking.Domain.Entities.Order.Order? entity)
    {
        Id = entity.Id;
        Description = entity.Description;
        CreatedAt = entity.CreatedAt;
        UpdatedAt = entity.UpdatedAt;
        Price = entity.Price;
    }

    public OrderDto()
    {
    }
}