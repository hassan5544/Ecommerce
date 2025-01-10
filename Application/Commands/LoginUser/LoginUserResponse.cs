using Application.Dtos.User;

namespace Application.Commands.LoginUser;

public record LoginUserResponse(bool IsSuccess, string Token ,UserDto? User = null ,IEnumerable<string>? Errors = null);