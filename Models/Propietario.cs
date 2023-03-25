using INMOBILIARIA.Models;

public class Propietario
{
    public int Id { get; set; }
    public long Dni { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public long Telefono { get; set; }
    public string Email { get; set; }
}