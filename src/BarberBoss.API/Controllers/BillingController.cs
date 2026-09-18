using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;

[Route("api/billing")]
[ApiController]
public class BillingController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllBillingsUseCase useCase,
        [FromQuery] RequestGetAllBillings request
        )
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [HttpPost] 
    public async Task<IActionResult> Register(
        [FromServices] IRegisterBillingUseCase useCase,
        [FromBody] RequestBillingJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
}
