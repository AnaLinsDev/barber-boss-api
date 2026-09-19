using BarberBoss.Domain.Entities;
using BarberBoss.Infrastructure.Helpers;

namespace BarberBoss.Domain.Repositories;
public interface IBillingsReadOnlyRepository
{
    Task<PaginationResult> GetAll(string? orderBy,
        string? order,
        string? filterBy,
        int page);
    Task<Billing?> GetById(Guid id);

    Task<List<Billing>> FilterByMonth(DateOnly month);
}
