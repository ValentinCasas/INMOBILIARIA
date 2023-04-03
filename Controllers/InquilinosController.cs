using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMOBILIARIA.Models;

namespace INMOBILIARIA.Controllers;

public class InquilinosController : Controller
{
    public InquilinosController()
    {

    }

    public IActionResult Index()
    {
        RepositorioInquilino repositorioInquilino = new RepositorioInquilino();
        ViewBag.lista = repositorioInquilino.GetInquilinos();
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Update()
    {
        return View();
    }



    [HttpPost]
    public IActionResult Create(Inquilino inquilino)
    {
        try
        {
            RepositorioInquilino repositorioInquilino = new RepositorioInquilino();
            int res = repositorioInquilino.Alta(inquilino);
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
            RepositorioInquilino repositorioInquilino = new RepositorioInquilino();
            Boolean res = repositorioInquilino.Baja(id);
            if (res == true)
            {
                return RedirectToAction("index");
            }
            else
            {
                TempData["Error"] = "Ocurrió un error al intentar eliminar el Inquilino.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "No se pudo borrar el Inquilino, probablemente este asociado a algun contrato";
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        try
        {
            RepositorioInquilino repositorioInquilino = new RepositorioInquilino();
            ViewBag.inquilino = repositorioInquilino.ObtenerPorId(id);
            if (ViewBag.inquilino != null)
            {
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
    public IActionResult UpdateInquilino(Inquilino inquilino)
    {
        try
        {
            RepositorioInquilino repositorioInquilino = new RepositorioInquilino();
            Boolean res = repositorioInquilino.Actualizar(inquilino);
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

