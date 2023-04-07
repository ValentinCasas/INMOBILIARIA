using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using INMOBILIARIA.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;


namespace INMOBILIARIA.Controllers
{
    /* [Authorize(Policy = "Administrador")] */
    [Authorize]
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
