using BarberBoss.Domain.Entities;

namespace BarberBoss.Infrastructure.Helpers;
public class PaginationResult
{
    public IList<Billing> Items { get; set; } = [];
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}
