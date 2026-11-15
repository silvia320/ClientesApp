using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClientesApp.Extensions;

public static class ValidationExtensions
{
    // Pasa los errores de FluentValidation al ModelState para que las vistas los muestren con asp-validation-for.
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
    }
}