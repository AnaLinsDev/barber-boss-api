using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Repositories;
internal class BillingRepository : IBillingsReadOnlyRepository, IBillingsWriteOnlyRepository, IBillingsUpdateOnlyRepository
{
    private readonly int PAGE_SIZE = 10;
    private readonly string ORDER_DESC = "desc";
    private readonly BillingDbContext _dbContext;
    public BillingRepository(BillingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Billing billing)
    {
        await _dbContext.Billings.AddAsync(billing);
    }

    public async Task<bool> Delete(Guid id)
    {
        var billing = await _dbContext.Billings.FirstOrDefaultAsync(billing => billing.Id == id);

        if (billing is null)
        {
            return false;
        }

        _dbContext.Billings.Remove(billing);

        return true;
    }

    public async Task<Billing?> GetById(Guid id)
    {
        return await _dbContext.Billings.AsNoTracking().FirstOrDefaultAsync(billing => billing.Id == id);
    }

    public void Update(Billing billing)
    {
        _dbContext.Billings.Update(billing);
    }

    public async Task<PaginationResult> GetAll(
    string? orderBy,
    string? order,
    string? filterBy,
    int page)
    {
        var query = _dbContext.Billings
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterBy))
        {
            query = QueryFilterBy(query, filterBy);
        }

        query = QueryOrderBy(query, orderBy, order);

        var totalItems = await query.CountAsync();

        var totalPages = (int)Math.Ceiling(
            (double)totalItems / PAGE_SIZE);

        var items = await query
            .Skip((page - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToListAsync();

        return new PaginationResult
        {
            Items = items,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }

    private IQueryable<Billing> QueryFilterBy(
        IQueryable<Billing> query,
        string filterBy)
    {
        return query.Where(b =>
            b.BarberName.Contains(filterBy) ||
            b.ClientName.Contains(filterBy) ||
            b.ServiceName.Contains(filterBy) ||
            b.Notes!.Contains(filterBy));
    }

    private IQueryable<Billing> QueryOrderBy(
        IQueryable<Billing> query,
        string? orderBy,
        string? order)
    {
        return orderBy?.ToLower() switch
        {
            "date" => order?.ToLower() == ORDER_DESC
                ? query.OrderByDescending(b => b.Date)
                : query.OrderBy(b => b.Date),

            "barbername" => order?.ToLower() == ORDER_DESC
                ? query.OrderByDescending(b => b.BarberName)
                : query.OrderBy(b => b.BarberName),

            "clientname" => order?.ToLower() == ORDER_DESC
                ? query.OrderByDescending(b => b.ClientName)
                : query.OrderBy(b => b.ClientName),

            "servicename" => order?.ToLower() == ORDER_DESC
                ? query.OrderByDescending(b => b.ServiceName)
                : query.OrderBy(b => b.ServiceName),

            "amount" => order?.ToLower() == ORDER_DESC
                ? query.OrderByDescending(b => b.Amount)
                : query.OrderBy(b => b.Amount),

            "paymentmethod" => order?.ToLower() == ORDER_DESC
                ? query.OrderByDescending(b => b.PaymentMethod)
                : query.OrderBy(b => b.PaymentMethod),

            "status" => order?.ToLower() == ORDER_DESC
                ? query.OrderByDescending(b => b.Status)
                : query.OrderBy(b => b.Status),

            "createdat" => order?.ToLower() == ORDER_DESC
                ? query.OrderByDescending(b => b.CreatedAt)
                : query.OrderBy(b => b.CreatedAt),

            "updatedat" => order?.ToLower() == ORDER_DESC
                ? query.OrderByDescending(b => b.UpdatedAt)
                : query.OrderBy(b => b.UpdatedAt),

            _ => query.OrderBy(b => b.Date)
        };
    }

}
