using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMOBILIARIA.Models;

namespace INMOBILIARIA.Controllers;

public class PagosController : Controller
{

    public PagosController() { }

    public IActionResult Index()
    {
        try
        {
            RepositorioPago repositorioPago = new RepositorioPago();
            ViewBag.lista = repositorioPago.GetPagos();
            return View();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public IActionResult Update()
    {
        return View();
    }

    public IActionResult Create()
    {
        RepositorioPago repositorioPago = new RepositorioPago();
        ViewBag.listaContratos = repositorioPago.GetContratos();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Pago pago)
    {
        try
        {
            RepositorioPago repositorioPago = new RepositorioPago();
            int res = repositorioPago.Alta(pago);
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
            RepositorioPago repositorioPago = new RepositorioPago();
            Boolean res = repositorioPago.Baja(id);
            if (res == true)
            {
                return RedirectToAction("index");
            }
            else
            {
                TempData["Error"] = "Ocurrió un error al intentar eliminar el pago.";
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
            RepositorioPago repositorioPago = new RepositorioPago();
            Pago pago = repositorioPago.ObtenerPorId(id);

            if (pago != null)
            {
                ViewBag.listaContratos = repositorioPago.GetContratos();
                ViewBag.pago = pago;
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
/*
    [HttpPost]
    public IActionResult UpdateInmueble(Inmueble inmueble)
    {
        try
        {
            RepositorioPagos repositorioPagos = new RepositorioPagos();
            Boolean res = repositorioPagos.Actualizar(inmueble);
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

*/
}