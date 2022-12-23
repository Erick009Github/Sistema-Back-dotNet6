using System.ComponentModel.DataAnnotations;

namespace SistemaErick2.Models;

public partial class CrearIngreso
{

    public int Idproveedor { get; set; }

    public int Idusuario { get; set; }

    public string TipoComprobante { get; set; } = null!;

    public string? SerieComprobante { get; set; }

    public string NumComprobante { get; set; } = null!;

    public int Impuesto { get; set; }

    public int Total { get; set; }

    //Propiedades Detalle
    [Required]
    public List<CrearDetalle> Detalles {get; set;}
}