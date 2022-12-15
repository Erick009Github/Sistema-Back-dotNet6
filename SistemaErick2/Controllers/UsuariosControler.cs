using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaErick2.Models;

namespace SistemaErick2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BdsistemaContext _context;

        public UsuariosController(BdsistemaContext context)
        {
            _context = context;
        }

         // GET: api/Usuarios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Usuario>> Listar()

        {
            var usuario = await _context.Usuarios.Include(u => u.IdrolNavigation).ToListAsync();

            return usuario.Select(u => new Usuario
            { 
              Idusuario= u.Idusuario,
              Idrol = u.Idrol,
              Nombre = u.Nombre,
              TipoDocumento = u.TipoDocumento,
              NumDocumento = u.NumDocumento,
              Direccion = u.Direccion,
              Telefono = u.Telefono,
              Email = u.Email,
              PasswordHash = u.PasswordHash,
              Condicion = u.Condicion,
              IdrolNavigation = u.IdrolNavigation
            });
        }

        
        // POST: api/Categorias/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearUsuario model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             var email = model.Email.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
            {
                return BadRequest("El email ya existe");
            }

            CrearPasswordHash(model.Password,out byte[] PasswordHash,out byte[] PasswordSalt);

            Usuario usuario = new Usuario
            {
              Idrol = model.Idrol,
              Nombre = model.Nombre,
              TipoDocumento = model.TipoDocumento,
              NumDocumento = model.NumDocumento,
              Direccion = model.Direccion,
              Telefono = model.Telefono,
              Email = model.Email.ToLower(),
              PasswordHash = PasswordHash,
              PasswordSalt = PasswordSalt,
              Condicion = true
            };

            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
           private void CrearPasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }

        }

          // PUT: api/Usuarios/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarUsuario model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Idusuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Idusuario == model.Idusuario);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Idrol = model.Idrol;
            usuario.Nombre = model.Nombre;
            usuario.TipoDocumento = model.TipoDocumento;
            usuario.NumDocumento = model.NumDocumento;
            usuario.Direccion = model.Direccion;
            usuario.Telefono = model.Telefono;
            usuario.Email = model.Email.ToLower();

              if (model.Act_Password== true)
            {
                CrearPasswordHash(model.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
                usuario.PasswordHash = PasswordHash;
                usuario.PasswordSalt = PasswordSalt;
            } 
             
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Usuarios/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Idusuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Condicion = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

         // PUT: api/Usuarios/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Idusuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Condicion = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }


         private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Idusuario == id);
        }

    }
}