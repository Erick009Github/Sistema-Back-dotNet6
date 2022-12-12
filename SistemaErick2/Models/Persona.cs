using System;
using System.Collections.Generic;

namespace SistemaErick2.Models;

public partial class Persona
{
    public int Idpersona { get; set; }

    public string TipoPersona { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? TipoDocumento { get; set; }

    public string? NumDocumento { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Ingreso> Ingresos { get; } = new List<Ingreso>();

    public virtual ICollection<Ventum> Venta { get; } = new List<Ventum>();
}
