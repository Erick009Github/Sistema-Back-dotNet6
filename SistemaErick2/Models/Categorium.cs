using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaErick2.Models;

public partial class Categorium
{
    public int Idcategoria { get; set; }

    public string? Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Condicion { get; set; }


    [JsonIgnore]
    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
}
