using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaErick2.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public int Idrol { get; set; }

    public string Nombre { get; set; } = null!;

    public string? TipoDocumento { get; set; }

    public string? NumDocumento { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;


    public bool? Condicion { get; set; }

    public virtual Rol IdrolNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

    [JsonIgnore]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
