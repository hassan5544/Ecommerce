using Application.Commands.User.LoginUser;
using Application.Commands.User.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registers a new user (Customer only).
    /// </summary>
    /// <param name="command">RegisterUserCommand containing user details.</param>
    /// <returns>Returns a UserWithTokenDto with user details and JWT token.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Register), result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    /// <summary>
    /// Logs in an existing user.
    /// </summary>
    /// <param name="command">LoginUserCommand containing email and password.</param>
    /// <returns>Returns a UserWithTokenDto with user details and JWT token.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
