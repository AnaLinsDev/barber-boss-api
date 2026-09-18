using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }


    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBillingsReadOnlyRepository, BillingRepository>();
        services.AddScoped<IBillingsWriteOnlyRepository, BillingRepository>();
        services.AddScoped<IBillingsUpdateOnlyRepository, BillingRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionPostgreSQL");

        services.AddDbContext<BillingDbContext>(config => config.UseNpgsql(connectionString));
    }
}