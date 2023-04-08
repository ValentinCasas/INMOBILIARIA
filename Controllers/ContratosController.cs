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

namespace INMOBILIARIA.Controllers;

[Authorize]
public class ContratosController : Controller
{

    public ContratosController() { }

    public IActionResult Index()
    {
        RepositorioContrato repositorioContrato = new RepositorioContrato();
        ViewBag.lista = repositorioContrato.GetContratos();
        ViewBag.pagos = repositorioContrato.GetPagos();
        return View();
    }

    public IActionResult Create()
    {
        RepositorioContrato repositorioContrato = new RepositorioContrato();
        ViewBag.listaInquilinos = repositorioContrato.GetInquilinos();
        ViewBag.listaInmuebles = repositorioContrato.GetInmuebles();
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
                TempData["Error"] = "Ocurrió un error al intentar eliminar el contrato, probablemente este asociado a algun pago.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
             TempData["Error"] = "Ocurrió un error al intentar eliminar el contrato, probablemente este asociado a algun pago.";
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
                ViewBag.listaInmuebles = repositorioContrato.GetInmuebles();
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
