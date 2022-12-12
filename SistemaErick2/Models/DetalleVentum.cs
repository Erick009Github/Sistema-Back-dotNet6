using System;
using System.Collections.Generic;

namespace SistemaErick2.Models;

public partial class DetalleVentum
{
    public int IddetalleVenta { get; set; }

    public int Idventa { get; set; }

    public int Idarticulo { get; set; }

    public int Cantidad { get; set; }

    public int Precio { get; set; }

    public int Descuento { get; set; }

    public virtual Articulo IdarticuloNavigation { get; set; } = null!;

    public virtual Ventum IdventaNavigation { get; set; } = null!;
}
