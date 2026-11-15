using Microsoft.AspNetCore.Mvc;

namespace ClientesApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}