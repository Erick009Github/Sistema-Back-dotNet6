using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaErick2.Models;

public partial class Ingreso
{
    public int Idingreso { get; set; }

    public int Idproveedor { get; set; }

    public int Idusuario { get; set; }

    public string TipoComprobante { get; set; } = null!;

    public string? SerieComprobante { get; set; }

    public string NumComprobante { get; set; } = null!;

    public DateTime FechaHora { get; set; }

    public int Impuesto { get; set; }

    public int Total { get; set; }

    public string Estado { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; } = new List<DetalleIngreso>();

    public virtual Persona IdproveedorNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null! ;
}
