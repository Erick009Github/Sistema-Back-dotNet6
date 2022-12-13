using System.ComponentModel.DataAnnotations;

namespace SistemaErick2.Models;

public partial class ActualizarUsuario
{
   
    public int Idusuario  {get;set;} 
    public int Idrol { get; set; }

    public string Nombre { get; set; } = null!;

    public string? TipoDocumento { get; set; }

    public string? NumDocumento { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Act_Password { get; set; }
 
}