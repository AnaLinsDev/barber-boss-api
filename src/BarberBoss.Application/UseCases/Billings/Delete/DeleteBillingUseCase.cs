
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.Billings.Delete;
public class DeleteBillingUseCase : IDeleteBillingUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillingsWriteOnlyRepository _repository;
    public DeleteBillingUseCase(IBillingsWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(Guid id)
    {
        var result = await _repository.Delete(id);

        if (result is false)
        {
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }
}
