using FluentValidation;
using ClientesApp.Application.Abstractions;
using ClientesApp.Application.Services;

namespace ClientesApp.Application;

public static class DependencyInjection
{
    // OCP: para agregar un modulo nuevo se registra aqui, sin tocar Program.cs ni lo existente.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registra automaticamente todos los AbstractValidator<T> del proyecto.
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<ILookupService, LookupService>();
        return services;
    }
}