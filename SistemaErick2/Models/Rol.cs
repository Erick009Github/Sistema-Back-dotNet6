using System;
using System.Collections.Generic;

namespace SistemaErick2.Models;

public partial class Rol
{
    public int Idrol { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Condicion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
