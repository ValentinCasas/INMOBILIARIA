using System;
using System.Collections.Generic;
using System.Linq;
using INMOBILIARIA.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using INMOBILIARIA.Controllers;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();
// Agrega la autenticación por cookies al sitio web, utilizando el esquema predeterminado de autenticación por cookies de .NET Core.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		// Establece la ruta para la página de inicio de sesión.
		options.LoginPath = "/Usuarios/Login";
		
		// Establece la ruta para la página de cierre de sesión.
		options.LogoutPath = "/Usuarios/Logout";
		
		// Establece la ruta para la página de acceso denegado.
		options.AccessDeniedPath = "/Home/Restringido";
	});
	
// Agrega la autorización al sitio web.
builder.Services.AddAuthorization(options =>
{
	// Agrega una política de autorización llamada "Administrador" que requiere el rol "Administrador" para acceder.
	options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
	
	// Agrega una política de autorización llamada "Empleado" que requiere el rol "Empleado" para acceder.
	options.AddPolicy("Empleado", policy => policy.RequireRole("Empleado"));
});

builder.Services.AddMvc();
builder.Services.AddSignalR();//añade signalR

var app = builder.Build();

// permit acceder a recursos de otro sitio web en un dominio diferente al que originó la solicitud
app.UseCors(x => x
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader());
// Uso de archivos estáticos (*.html, *.css, *.js, etc.)
app.UseStaticFiles();
app.UseRouting();
// Permitir cookies
app.UseCookiePolicy(new CookiePolicyOptions
{
	MinimumSameSitePolicy = SameSiteMode.None,
});
// Habilitar autenticación
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.MapControllerRoute(	name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
