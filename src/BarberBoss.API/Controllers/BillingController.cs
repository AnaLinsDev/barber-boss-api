using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Application.UseCases.Billings.GetById;
using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;

[Route("api/billing")]
[ApiController]
public class BillingController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllBillingsUseCase useCase,
        [FromQuery] RequestGetAllBillings request
        )
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseBillingJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterBillingUseCase useCase,
        [FromBody] RequestBillingJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseBillingJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetBillingByIdUseCase useCase,
        [FromRoute] Guid id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }
}
