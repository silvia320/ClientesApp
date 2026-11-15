using System.Globalization;
using FluentValidation;
using ClientesApp.Application;
using ClientesApp.Infrastructure;

// .env
try { DotNetEnv.Env.NoClobber().TraversePath().Load(); } catch (FileNotFoundException) { }

// Fluent Validation, idioma español
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    // todas las validaciones
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.ApplyMigrationsAsync();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(e => e.Run(ctx => ctx.Response.WriteAsync("Error inesperado")));
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();