using BarberBoss.Application.UseCases.Billings.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;

[Route("api/billing")]
[ApiController]
public class BillingController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllBillingsUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Billings.Count > 0)
        {
            return Ok(response);
        }

        return NoContent();
    }
}
