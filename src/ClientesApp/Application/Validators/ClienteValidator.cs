using FluentValidation;
using ClientesApp.Application.Abstractions;
using ClientesApp.Application.Dtos;
using ClientesApp.Domain.Entities;

namespace ClientesApp.Application.Validators;

// SRP: toda la validacion de Cliente vive aqui, ni en el DTO ni en el controlador.
public class ClienteValidator : AbstractValidator<ClienteDto>
{
    public ClienteValidator(
        IClienteRepository clientes,
        IRepository<Ciudad> ciudades,
        IRepository<TipoCliente> tiposCliente,
        IRepository<Genero> generos)
    {
        RuleFor(x => x.Cedula).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("La cédula es obligatoria")
            .Matches(@"^\d{10}$").WithMessage("La cédula debe tener exactamente 10 dígitos")
            .MustAsync(async (dto, cedula, ct) => !await clientes.ExistsByCedulaAsync(cedula, dto.Id, ct))
            .WithMessage("Ya existe un cliente con esa cédula");

        RuleFor(x => x.Nombres).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Los nombres son obligatorios")
            .MaximumLength(80).WithMessage("Máximo 80 caracteres");

        RuleFor(x => x.Apellidos).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Los apellidos son obligatorios")
            .MaximumLength(80).WithMessage("Máximo 80 caracteres");

        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("El correo es obligatorio")
            .MaximumLength(120).WithMessage("Máximo 120 caracteres")
            .EmailAddress().WithMessage("El correo no tiene un formato válido")
            .MustAsync(async (dto, email, ct) => !await clientes.ExistsByEmailAsync(email, dto.Id, ct))
            .WithMessage("Ya existe un cliente con ese correo");

        RuleFor(x => x.Telefono).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("El teléfono es obligatorio")
            .Matches(@"^[0-9+\-\s]{7,15}$").WithMessage("El teléfono debe tener entre 7 y 15 caracteres (dígitos, +, -)");

        RuleFor(x => x.FechaNacimiento).Cascade(CascadeMode.Stop)
            .Must(f => f >= new DateOnly(1900, 1, 1)).WithMessage("La fecha de nacimiento no es válida")
            .Must(f => f <= DateOnly.FromDateTime(DateTime.Today.AddYears(-18))).WithMessage("El cliente debe ser mayor de 18 años");

        RuleFor(x => x.Direccion)
            .MaximumLength(200).WithMessage("Máximo 200 caracteres");

        RuleFor(x => x.GeneroId).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Seleccione un género")
            .MustAsync(async (id, ct) => await generos.GetByIdAsync(id, ct) is not null)
            .WithMessage("El género seleccionado no existe");

        RuleFor(x => x.CiudadId).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Seleccione una ciudad")
            .MustAsync(async (id, ct) => await ciudades.GetByIdAsync(id, ct) is not null)
            .WithMessage("La ciudad seleccionada no existe");

        RuleFor(x => x.TipoClienteId).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Seleccione un tipo de cliente")
            .MustAsync(async (id, ct) => await tiposCliente.GetByIdAsync(id, ct) is not null)
            .WithMessage("El tipo de cliente seleccionado no existe");
    }
}