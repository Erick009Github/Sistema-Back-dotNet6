using System;
using System.Collections.Generic;

namespace SistemaErick2.Models;

public partial class CrearDetalleVenta
{
   
    public int Idarticulo { get; set; }

    public int Cantidad { get; set; }

    public int Precio { get; set; }

     public int Descuento { get; set; }

}