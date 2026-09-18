using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Billings.Register;
public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IBillingsWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterBillingUseCase(
        IBillingsWriteOnlyRepository repository, 
        IMapper mapper, IUnitOfWork 
        unitOfWork)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseBillingJson> Execute(RequestBillingJson request)
    {

        var entity = _mapper.Map<Billing>(request);

        await _repository.Add(entity);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseBillingJson>(entity);
    }
}
