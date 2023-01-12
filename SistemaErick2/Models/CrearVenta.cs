using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaErick2.Models;

public partial class CrearVenta
{

    public int Idcliente { get; set; }

    public int Idusuario { get; set; }

    public string TipoComprobante { get; set; } = null!;

    public string? SerieComprobante { get; set; }

    public string NumComprobante { get; set; } = null!;

    public int Impuesto { get; set; }

    public int Total { get; set; }

    [Required]
    
    public List<CrearDetalleVenta> Detalles  {get; set;}
    
}