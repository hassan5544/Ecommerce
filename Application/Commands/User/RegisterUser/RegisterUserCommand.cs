using MediatR;

namespace Application.Commands.User.RegisterUser;

public record RegisterUserCommand(string FullName , string Email , string Password) : IRequest<RegisterUserResponse>;