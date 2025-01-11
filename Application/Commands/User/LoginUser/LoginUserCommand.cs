using MediatR;

namespace Application.Commands.User.LoginUser;

public class LoginUserCommand : IRequest<LoginUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
