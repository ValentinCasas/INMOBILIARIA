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
        ViewBag.lista = repositorioContrato.GetContratos();
        return View();
    }

    public IActionResult Create()
    {
        RepositorioContrato repositorioContrato = new RepositorioContrato();
        ViewBag.listaInquilinos = repositorioContrato.GetInquilinos();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Contrato contrato)
    {
        try
        {
            RepositorioContrato repositorioContrato = new RepositorioContrato();
            int res = repositorioContrato.Alta(contrato);
            if (res > 0)
            {
                return RedirectToAction("index");
            }
            else
            {
                TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
                return RedirectToAction("Create");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
            return RedirectToAction("Create");
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        try
        {
            RepositorioContrato repositorioContrato = new RepositorioContrato();
            Boolean res = repositorioContrato.Baja(id);
            if (res == true)
            {
                return RedirectToAction("index");
            }
            else
            {
                TempData["Error"] = "Ocurrió un error al intentar eliminar el inmueble.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "No se pudo borrar el inmueble, para mas informacion contactarse con el administrador";
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        try
        {
            RepositorioContrato repositorioContrato = new RepositorioContrato();
            Contrato contrato = repositorioContrato.ObtenerPorId(id);
            if (contrato != null)
            {
                ViewBag.listaInquilinos = repositorioContrato.GetInquilinos();
                ViewBag.contrato = contrato;
                return View("Update");
            }
            else
            {
                TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult UpdateContrato(Contrato contrato)
    {
        try
        {
            RepositorioContrato repositorioContrato = new RepositorioContrato();
            Boolean res = repositorioContrato.Actualizar(contrato);
            if (res == true)
            {
                return RedirectToAction("index");
            }
            else
            {
                TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
                return RedirectToAction("Update");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
            return RedirectToAction("Update");
        }
    }


}
