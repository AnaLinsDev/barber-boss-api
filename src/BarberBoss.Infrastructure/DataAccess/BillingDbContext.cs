using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess;
public class BillingDbContext : DbContext
{
    public BillingDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Billing> Billings { get; set; }
}
