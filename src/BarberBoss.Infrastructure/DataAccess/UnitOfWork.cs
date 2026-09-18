using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infrastructure.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly BillingDbContext _dbContext;
    public UnitOfWork(BillingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit() => await _dbContext.SaveChangesAsync();
}