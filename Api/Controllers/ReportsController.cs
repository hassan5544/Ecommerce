using Application.Queries.orderQueries.MostSoldProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get the most sold products report.
    /// </summary>
    /// <param name="topN">Number of top products to return.</param>
    /// <returns>A list of most sold products.</returns>
    [HttpGet("most-sold-products")]
    public async Task<IActionResult> GetMostSoldProducts([FromQuery] int topN = 10, CancellationToken cancellationToken = default)
    {
        var query = new GetMostSoldProductsQuery
        {
            TopN = topN
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}