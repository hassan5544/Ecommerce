using MediatR;

namespace Application.Commands.LoginUser;

public record LoginUserCommand(string UserName , string Password) : IRequest<LoginUserResponse>;