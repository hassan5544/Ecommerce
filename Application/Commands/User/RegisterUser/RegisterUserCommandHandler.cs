using Application.Commands.User.RegisterUser;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.User.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if(await _userRepository.EmailExistsAsync(request.Email))
        {
            throw new Exception("Email already exists");   
        }
        var user = Users.CreateCustomer(request.FullName, request.Email, request.Password, _passwordHasher);
        
        await _userRepository.AddUserAsync(user);
        
        string token = _jwtTokenGenerator.GenerateToken(user);
        RegisterUserResponse response = new RegisterUserResponse
        {
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            Token = token
        };
        return response;
    }
}