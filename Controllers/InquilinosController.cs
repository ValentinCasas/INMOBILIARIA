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
        var lista = repositorioInquilino.GetInquilinos();
        return View(lista);
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
        RepositorioInquilino repositorioInquilino = new RepositorioInquilino();
        int res = repositorioInquilino.Alta(inquilino);
        if (res > 0)
        {
            return RedirectToAction("index");
        }
        else
        {
            return View(inquilino);
        }
    }


    [HttpGet]
    public IActionResult Delete(int id)
    {
        RepositorioInquilino repositorioInquilino = new RepositorioInquilino();
        Boolean res = repositorioInquilino.Baja(id);
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
        RepositorioInquilino repositorioInquilino = new RepositorioInquilino();
        Inquilino inquilino = repositorioInquilino.ObtenerPorId(id);
        if (inquilino != null)
        {
            return View("Update", inquilino);
        }
        else
        {
            return View(id);
        }
    }

    [HttpPost]
    public IActionResult UpdateInquilino(Inquilino inquilino)
    {
        RepositorioInquilino repositorioInquilino = new RepositorioInquilino();
        Boolean res = repositorioInquilino.Actualizar(inquilino);
        if (res == true)
        {
            return RedirectToAction("index");
        }
        else
        {
            return View(inquilino);
        }
    }


}

