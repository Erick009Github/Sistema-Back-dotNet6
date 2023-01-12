using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    public string Email { get; set; }
     
    [JsonIgnore]
    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
    
    [JsonIgnore]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
