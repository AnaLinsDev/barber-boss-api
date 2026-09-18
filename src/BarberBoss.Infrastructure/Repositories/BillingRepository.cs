using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Repositories;
internal class BillingRepository : IBillingsReadOnlyRepository, IBillingsWriteOnlyRepository, IBillingsUpdateOnlyRepository
{
    private readonly BillingDbContext _dbContext;
    public BillingRepository(BillingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Billing billing)
    {
        await _dbContext.Billings.AddAsync(billing);
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Billing>> GetAll()
    {
        return await _dbContext.Billings.AsNoTracking().ToListAsync();
    }

    public Task<Billing?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Update(Billing expense)
    {
        throw new NotImplementedException();
    }
}
