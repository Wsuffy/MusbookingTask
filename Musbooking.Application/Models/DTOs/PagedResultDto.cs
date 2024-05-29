namespace Musbooking.Application.Models.DTOs;

public class PagedResultDto<T>
{
    public IReadOnlyList<T> Items { get; set; }
    public int Count { get; set; }

    public PagedResultDto(IReadOnlyList<T> items, int totalCount)
    {
        Items = items;
        Count = totalCount;
    }

    public PagedResultDto()
    {
    }
}