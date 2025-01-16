using Application.Commands.order.CreateOrder;
using Application.Commands.order.DeleteOrder;
using Application.Commands.order.UpdateOrderStatus;
using Application.Dtos.Order;
using Application.Queries.orderQueries.GetOrdersFilter;
using Application.Queries.orderQueries.GetSingleOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Get Order by Id
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin,Customer")]

    public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
    {
        var query = new GetOrderByIdQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Customer")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders(
        [FromQuery] Guid? customerId,
        [FromQuery] DateTime? orderDateFrom,
        [FromQuery] string status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sort = "OrderDate")
    {
        var query = new GetOrderQuery
        {
            CustomerId = customerId,
            OrderDateFrom = orderDateFrom,
            Status = status,
            Page = page,
            PageSize = pageSize,
            Sort = sort
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }
    // Update Order Status
    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusCommand command)
    {
        if (id != command.OrderId)
        {
            return BadRequest("Order ID mismatch.");
        }

        try
        {
            await _mediator.Send(command);
            return NoContent(); // No content to return after a successful update
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Delete Order
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteOrder(DeleteOrderCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return NoContent(); // No content to return after successful deletion
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin,Customer")]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new { id = result }, result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
 
    }

}