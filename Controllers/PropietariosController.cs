using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMOBILIARIA.Models;

namespace INMOBILIARIA.Controllers
{
    public class PropietariosController : Controller
    {
        public PropietariosController()
        {
        }

        public IActionResult Index()
        {
            RepositorioPropietario repositorioPropietario = new RepositorioPropietario();
            ViewBag.lista = repositorioPropietario.GetPropietarios();
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
        public IActionResult Create(Propietario propietario)
        {
            try
            {
                RepositorioPropietario repositorioPropietario = new RepositorioPropietario();
                int res = repositorioPropietario.Alta(propietario);
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
            catch(Exception ex)
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
                RepositorioPropietario repositorioPropietario = new RepositorioPropietario();
                Boolean res = repositorioPropietario.Baja(id);
                if (res == true)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["Error"] = "Ocurrió un error al intentar eliminar el propietario.";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                TempData["Error"] = "No se pudo borrar el propietario, probablemente este asociado a algun inmueble";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                RepositorioPropietario repositorioPropietario = new RepositorioPropietario();
                ViewBag.propietario = repositorioPropietario.ObtenerPorId(id);
                if (ViewBag.propietario != null)
                {
                    return View("Update");
                }
                else
                {
                    TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult UpdatePropietario(Propietario propietario)
        {
            try
            {
                RepositorioPropietario respositorioPersona = new RepositorioPropietario();
                Boolean res = respositorioPersona.Actualizar(propietario);
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
            catch(Exception ex)
            {
                TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
                return RedirectToAction("Update");
            }
        }
    }
}
