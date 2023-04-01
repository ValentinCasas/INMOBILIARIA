using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMOBILIARIA.Models;

namespace INMOBILIARIA.Controllers;

public class ContratosController : Controller
{

    public ContratosController() { }

    public IActionResult Index()
    {
        RepositorioContrato repositorioContrato = new RepositorioContrato();
        var lista = repositorioContrato.GetContratos();
        return View(lista);
    }

}
