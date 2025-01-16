using Application.Commands.product.CreateProduct;
using Application.Commands.product.DeleteProduct;
using Application.Commands.product.UpdateProduct;
using Application.Queries.productQueries.GetProductFilter;
using Application.Queries.productQueries.GetSingleProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new product.
    /// Accessible only by Admins.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
 
    }

    /// <summary>
    /// Updates an existing product.
    /// Accessible only by Admins.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Product ID in the path does not match the ID in the body.");
        }

        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Deletes a product.
    /// Accessible only by Admins.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Gets a single product by its ID.
    /// Accessible by all authenticated users.
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    /// <summary>
    /// Get filtered products with pagination and sorting.
    /// </summary>
    /// <param name="category">Category to filter by.</param>
    /// <param name="isAvailable">Filter by availability.</param>
    /// <param name="priceFrom">Minimum price.</param>
    /// <param name="priceTo">Maximum price.</param>
    /// <param name="page">Page number.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="sort">Sort by field.</param>
    /// <returns>List of products matching the criteria.</returns>
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredProducts(
        [FromQuery] string category = "",
        [FromQuery] bool isAvailable = true,
        [FromQuery] decimal priceFrom = 0,
        [FromQuery] decimal priceTo = decimal.MaxValue,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sort = "price")
    {
        var query = new GetProductsQuery
        {
            Category = category,
            IsAvailable = isAvailable,
            PriceFrom = priceFrom,
            PriceTo = priceTo,
            Page = page,
            PageSize = pageSize,
            Sort = sort
        };

        var products = await _mediator.Send(query);
        return Ok(products);
    }
}