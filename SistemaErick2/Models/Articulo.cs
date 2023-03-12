using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


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

    public int? CantidadVendida { get; set; }

    public int? CantidadComprada { get; set; }

    public bool? Condicion { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; } = new List<DetalleIngreso>();
    [JsonIgnore]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Categorium? IdcategoriaNavigation { get; set; }
}
