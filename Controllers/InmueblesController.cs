using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMOBILIARIA.Models;

namespace INMOBILIARIA.Controllers;

public class InmueblesController : Controller
{

    public InmueblesController() { }

    public IActionResult Index()
    {
        RepositorioInmueble repositorioInmueble = new RepositorioInmueble();
        var lista = repositorioInmueble.GetInmuebles();
        return View(lista);
    }
    public IActionResult Update()
    {
        return View();
    }

    public IActionResult Create()
    {
        RepositorioInmueble repositorioInmueble = new RepositorioInmueble();
        var listaPropietarios = repositorioInmueble.GetPropietarios();
        var listaContratos = repositorioInmueble.GetContratos();
        var model = new { ListaPropietarios = listaPropietarios, ListaContratos = listaContratos };
        return View(model);
    }

    [HttpPost]
    public IActionResult Create(Inmueble inmueble)
    {
        RepositorioInmueble repositorioInquilino = new RepositorioInmueble();
        int res = repositorioInquilino.Alta(inmueble);
        if (res > 0)
        {
            return RedirectToAction("index");
        }
        else
        {
            return View(inmueble);
        }
    }


    [HttpGet]
    public IActionResult Delete(int id)
    {
        RepositorioInmueble repositorioInmueble = new RepositorioInmueble();
        Boolean res = repositorioInmueble.Baja(id);
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
        RepositorioInmueble repositorioInmueble = new RepositorioInmueble();
        Inmueble inmueble = repositorioInmueble.ObtenerPorId(id);
        if (inmueble != null)
        {

            var listaPropietarios = repositorioInmueble.GetPropietarios();
            var listaContratos = repositorioInmueble.GetContratos();
            var model = new { ListaPropietarios = listaPropietarios, ListaContratos = listaContratos, Inmueble = inmueble };

            return View("Update", model);
        }
        else
        {
            return View(id);
        }
    }

    [HttpPost]
    public IActionResult UpdateInmueble(Inmueble inmueble)
    {
        RepositorioInmueble repositorioInmueble = new RepositorioInmueble();
        Boolean res = repositorioInmueble.Actualizar(inmueble);
        if (res == true)
        {
            return RedirectToAction("index");
        }
        else
        {
            return View(inmueble);
        }
    }


}