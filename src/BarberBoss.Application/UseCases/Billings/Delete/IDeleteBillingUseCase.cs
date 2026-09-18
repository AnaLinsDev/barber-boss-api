using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Billings.Delete;
public interface IDeleteBillingUseCase
{
    Task Execute(Guid Id);
}
