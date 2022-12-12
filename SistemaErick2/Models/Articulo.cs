using System;
using System.Collections.Generic;



namespace SistemaErick2.Models;

public partial class Articulo
{
   
    public int Idarticulo { get; set; }
    
    public int? Idcategoria { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; } 

 
    public int? PrecioVenta { get; set; }


    public int? Stock { get; set; }

    public string? Descripcion { get; set; }

    public bool? Condicion { get; set; }

    public virtual ICollection<DetalleIngreso> DetalleIngresos { get; } = new List<DetalleIngreso>();

    public virtual ICollection<DetalleVentum> DetalleVenta { get; } = new List<DetalleVentum>();

    public virtual Categorium? IdcategoriaNavigation { get; set; }
}
