using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IBillingsReadOnlyRepository
{
    Task<List<Billing>> GetAll();
    Task<Billing?> GetById(Guid id);
}
