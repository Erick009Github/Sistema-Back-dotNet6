using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaErick2.Models;

public partial class Rol
{
    public int Idrol { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Condicion { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Usuario> Usuarios { get; set;} = new List<Usuario>();
}
