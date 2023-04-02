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

    public IActionResult Create()
    {
        RepositorioContrato repositorioContrato = new RepositorioContrato();
        var listaInquilinos = repositorioContrato.GetInquilinos();
        var model = new { ListaInquilinos = listaInquilinos };
        return View(model);
    }

    [HttpPost]
    public IActionResult Create(Contrato contrato)
    {
        RepositorioContrato repositorioContrato = new RepositorioContrato();
        int res = repositorioContrato.Alta(contrato);
        if (res > 0)
        {
            return RedirectToAction("index");
        }
        else
        {
            return View(contrato);
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        RepositorioContrato repositorioContrato = new RepositorioContrato();
        Boolean res = repositorioContrato.Baja(id);
        if (res == true)
        {
            return RedirectToAction("index");
        }
        else
        {
            return View(id);
        }
    }


}
