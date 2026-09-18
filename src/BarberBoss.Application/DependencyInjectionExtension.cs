
using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Application.UseCases.Billings.Register;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    public static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
       {
           config.AddProfile<AutoMapping>();
       });
    }

    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IGetAllBillingsUseCase, GetAllBillingsUseCase>();
        services.AddScoped<IRegisterBillingUseCase, RegisterBillingUseCase>();
    }
}
