using INMOBILIARIA.Models;

public class Inmueble
{

    public int Id { get; set; }
    public Propietario Propietario { get; set; }
    public Contrato Contrato { get; set; }
    public string? Direccion { get; set; }
    public string? Uso { get; set; } //comercial o residencial
    public string? Tipo { get; set; } //local, deposito, casa, departamento, etc
    public int CantidadAmbientes { get; set; }
    public string? Coordenadas { get; set; }
    public decimal PrecioInmueble { get; set; }
    public string? Estado { get; set; } //disponible no disponible

}