using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IBillingsUpdateOnlyRepository
{
    Task<Billing?> GetById(Guid id);
    void Update(Billing expense);
}
