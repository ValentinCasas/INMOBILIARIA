using INMOBILIARIA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
public class Contrato
{

    public int Id { get; set; }

    public int IdInquilino { get; set; }
    [ForeignKey(nameof(IdInquilino))]
    public Inquilino Inquilino { get; set; }


    public List<Pago> Pago { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime FechaFinalizacion { get; set; }
    public decimal MontoAlquilerMensual { get; set; }
    public Boolean Activo { get; set; }


    public override string ToString()
    {
        return $"IdInquilino: {IdInquilino}| Fecha inicio: {FechaInicio}| Fecha fin: {FechaFinalizacion}| Monto: {MontoAlquilerMensual}| Activo: {Activo}";
    }

}

