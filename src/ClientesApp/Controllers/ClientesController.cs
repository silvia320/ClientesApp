using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClientesApp.Application.Abstractions;
using ClientesApp.Application.Dtos;
using ClientesApp.Extensions;

namespace ClientesApp.Controllers;

// Controlador delgado: solo HTTP. La logica esta en IClienteService y las reglas en ClienteValidator.
public class ClientesController : Controller
{
    private readonly IClienteService _service;
    private readonly ILookupService _lookups;
    private readonly IValidator<ClienteDto> _validator;

    public ClientesController(IClienteService service, ILookupService lookups, IValidator<ClienteDto> validator)
    {
        _service = service;
        _lookups = lookups;
        _validator = validator;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _service.GetAllAsync(ct));

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        await LoadLookupsAsync(ct);
        return View(new ClienteDto { FechaNacimiento = DateOnly.FromDateTime(DateTime.Today.AddYears(-18)) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClienteDto dto, CancellationToken ct)
    {
        dto.Id = 0;
        (await _validator.ValidateAsync(dto, ct)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            await LoadLookupsAsync(ct);
            return View(dto);
        }

        await _service.CreateAsync(dto, ct);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var dto = await _service.GetByIdAsync(id, ct);
        if (dto is null) return NotFound();
        await LoadLookupsAsync(ct);
        return View(dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClienteDto dto, CancellationToken ct)
    {
        dto.Id = id;
        (await _validator.ValidateAsync(dto, ct)).AddToModelState(ModelState);
        if (!ModelState.IsValid)
        {
            await LoadLookupsAsync(ct);
            return View(dto);
        }

        return await _service.UpdateAsync(id, dto, ct) ? RedirectToAction(nameof(Index)) : NotFound();
    }

    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var dto = await _service.GetByIdAsync(id, ct);
        return dto is null ? NotFound() : View(dto);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadLookupsAsync(CancellationToken ct)
    {
        ViewBag.Ciudades = new SelectList(await _lookups.GetCiudadesAsync(ct), "Id", "Text");
        ViewBag.TiposCliente = new SelectList(await _lookups.GetTiposClienteAsync(ct), "Id", "Text");
        ViewBag.Generos = new SelectList(await _lookups.GetGenerosAsync(ct), "Id", "Text");
    }
}