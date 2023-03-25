using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMOBILIARIA.Models;

namespace INMOBILIARIA.Controllers;

public class PropietariosController : Controller
{
    public PropietariosController()
    {

    }

    public IActionResult Index()
    {
        RepositorioPropietario repositorioPropietario = new RepositorioPropietario();
        var lista = repositorioPropietario.GetPropietarios();
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
    public IActionResult Create(Propietario propietario)
    {
        RepositorioPropietario repositorioPropietario = new RepositorioPropietario();
        int res = repositorioPropietario.Alta(propietario);
        if (res > 0)
        {
            return RedirectToAction("index");
        }
        else
        {
            return View(propietario);
        }
    }


    [HttpGet]
    public IActionResult Delete(int id)
    {
        RepositorioPropietario repositorioPropietario = new RepositorioPropietario();
        Boolean res = repositorioPropietario.Baja(id);
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
        RepositorioPropietario repositorioPropietario = new RepositorioPropietario();
        Propietario propietario = repositorioPropietario.ObtenerPorId(id);
        if (propietario != null)
        {
            return View("Update", propietario);
        }
        else
        {
            return View(id);
        }
    }

    [HttpPost]
    public IActionResult UpdatePropietario(Propietario propietario)
    {
        RepositorioPropietario respositorioPersona = new RepositorioPropietario();
        Boolean res = respositorioPersona.Actualizar(propietario);
        if (res == true)
        {
            return RedirectToAction("index");
        }
        else
        {
            return View(propietario);
        }
    }


}

