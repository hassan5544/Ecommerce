using MediatR;

namespace Application.Commands.RegisterUser;

public record RegisterUserCommand(string FullName , string Email , string Password) : IRequest<RegisterUserResponse>;