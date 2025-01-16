using System.Reflection;
using Application.Commands.User.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(RegisterUserCommandHandler).Assembly));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}