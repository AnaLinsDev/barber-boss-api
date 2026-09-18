using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Billings.GetAll;
public class GetAllBillingsUseCase : IGetAllBillingsUseCase
{
    private readonly IBillingsReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetAllBillingsUseCase(IBillingsReadOnlyRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ResponseBillingsJson> Execute(RequestGetAllBillings request)
    {
        var result = await _repository.GetAll(
         request.OrderBy,
         request.Order,
         request.FilterBy,
         request.Page);

        return new ResponseBillingsJson
        {
            Billings = _mapper.Map<List<ResponseShortBillingJson>>(result.Items),
            TotalPages = result.TotalPages,
            CurrentPage = result.CurrentPage,
            TotalItems = result.TotalItems,
        };
    }
        
}