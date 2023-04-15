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

            if (contrato.FechaInicio >= contrato.FechaFinalizacion)
            {
                TempData["Error"] = "La fecha de inicio debe ser anterior a la fecha de finalización";
                return RedirectToAction("Create");
            }

            if (contrato.FechaInicio < DateTime.Now.Date)
            {
                TempData["Error"] = "La fecha de inicio debe ser igual o posterior a la fecha actual";
                return RedirectToAction("Create");
            }

            RepositorioContrato repositorioContrato = new RepositorioContrato();

            if (repositorioContrato.ExisteSolapamientoContratosActivos(contrato.IdInmueble, contrato.FechaInicio, contrato.FechaFinalizacion))
            {
                TempData["Error"] = "Ya existe un contrato activo en el mismo inmueble que se solapa con las fechas del nuevo contrato";
                return RedirectToAction("Create");
            }

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
     RepositorioContrato repositorioContrato = new RepositorioContrato();
    try
    {
        if (contrato.FechaFinalizacion < contrato.FechaInicio)
        {
            TempData["Error"] = "La fecha de finalización no puede ser anterior a la fecha de inicio";
            return RedirectToAction("Update", new { id = contrato.Id });
        }
        else
        {
            TimeSpan tiempoTranscurrido = DateTime.Now.Date - contrato.FechaInicio;
            if (contrato.FechaFinalizacion < DateTime.Now.Date && !contrato.Activo)
            {
                contrato.Activo = false;
                // Si la fecha de finalización es anterior a la fecha actual, se debe calcular la multa
                TimeSpan tiempoAlquiler = contrato.FechaFinalizacion - contrato.FechaInicio;
                // Actualizar el tiempo transcurrido para tener en cuenta la fecha de finalización
                tiempoTranscurrido = contrato.FechaFinalizacion - contrato.FechaInicio;

                // Se verifica si se cumplió menos de la mitad del tiempo original de alquiler
                if (tiempoTranscurrido.TotalDays < (tiempoAlquiler.TotalDays / 2))
                {
                }
                TempData["Mensaje"] = "Debe pagar 2 (dos) meses extra de alquiler: $" + contrato.MontoAlquilerMensual*2;
                repositorioContrato.AgregarMulta(contrato.IdInquilino, contrato.MontoAlquilerMensual*2);
            }
            else if (contrato.FechaFinalizacion >= DateTime.Now.Date && !contrato.Activo)
            {
                TempData["Mensaje"] = "Debe pagar 1 (un) mes extra de alquiler: $" + contrato.MontoAlquilerMensual;
                repositorioContrato.AgregarMulta(contrato.IdInquilino, contrato.MontoAlquilerMensual);
            }
            if (contrato.Activo)
            {
                TempData.Remove("Error"); // Se elimina el mensaje de error si el contrato está activo
            }
        }

       
        Boolean res = repositorioContrato.Actualizar(contrato);
        if (res == true)
        {
            return RedirectToAction("index");
        }
        else
        {
            TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
            return RedirectToAction("Update", new { id = contrato.Id });
        }
    }
    catch (Exception ex)
    {
        TempData["Error"] = "Por favor llene todos los campos y ponga los datos correctamente";
        return RedirectToAction("Update", new { id = contrato.Id });
    }
}




}
