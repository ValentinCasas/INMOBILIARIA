using INMOBILIARIA.Models;


public class Contrato
{

    public int Id { get; set; }
    public Inquilino Inquilino { get; set; }
    public List<Pago> Pagos { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFinalizacion { get; set; }
    public decimal MontoAlquilerMensual { get; set; }
    public Boolean Activo { get; set; }

}

