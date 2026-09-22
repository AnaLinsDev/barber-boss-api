using AutoMapper;
using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;

namespace BarberBoss.Application.UseCases.Billings.Update;
public class UpdateBillingUseCase : IUpdateBillingUseCase
{

    private readonly IBillingsUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateBillingUseCase(
        IBillingsUpdateOnlyRepository repository,
        IMapper mapper, IUnitOfWork
        unitOfWork)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid id, RequestBillingJson request)
    {
        Validate(request);

        var billing = await _repository.GetById(id);

        if (billing == null)
        {
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
        }

        _mapper.Map(request, billing);

        billing.UpdatedAt = DateTime.UtcNow;
        billing.CreatedAt = DateTime.UtcNow;

        _repository.Update(billing);
        await _unitOfWork.Commit();
    }

    private void Validate(RequestBillingJson request)
    {
        var validator = new BillingValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
