namespace Application.Commands.User.LoginUser;

public record LoginUserResponse
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
}