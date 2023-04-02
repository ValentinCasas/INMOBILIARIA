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

    [HttpGet]
    public IActionResult Update(int id)
    {
        RepositorioContrato repositorioContrato = new RepositorioContrato();
        Contrato contrato = repositorioContrato.ObtenerPorId(id);
        if (contrato != null)
        {

            var listaInquilinos = repositorioContrato.GetInquilinos();
            var model = new { ListaInquilinos = listaInquilinos, Contrato = contrato};

            return View("Update", model);
        }
        else
        {
            return View(id);
        }
    }

    [HttpPost]
    public IActionResult UpdateContrato(Contrato contrato)
    {
        RepositorioContrato repositorioContrato = new RepositorioContrato();
        Boolean res = repositorioContrato.Actualizar(contrato);
        if (res == true)
        {
            return RedirectToAction("index");
        }
        else
        {
            return View(contrato);
        }
    }


}
