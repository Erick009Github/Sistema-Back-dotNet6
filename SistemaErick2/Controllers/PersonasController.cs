using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaErick2.Models;

namespace SistemaErick2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {

        private readonly BdsistemaContext _context;

        public PersonasController(BdsistemaContext context)
        {
            _context = context;
        }

        // GET: api/Personas/ListarClientes
        [Authorize(Roles = "Vendedor,Administrador,SuperUsuario")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<Persona>> ListarClientes()

        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Cliente").ToListAsync();

            return persona.Select(p => new Persona
            {
                Idpersona = p.Idpersona,
                TipoPersona = p.TipoPersona,
                Nombre = p.Nombre,
                TipoDocumento = p.TipoDocumento,
                NumDocumento = p.NumDocumento,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Email = p.Email

            });
        }


        // GET: api/Personas/ListarProveedores
        [Authorize(Roles ="Bodeguero,Administrador,SuperUsuario")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<Persona>> ListarProveedores()

        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Proveedor").ToListAsync();

            return persona.Select(p => new Persona
            {
                Idpersona = p.Idpersona,
                TipoPersona = p.TipoPersona,
                Nombre = p.Nombre,
                TipoDocumento = p.TipoDocumento,
                NumDocumento = p.NumDocumento,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Email = p.Email

            });
        }

        //GET: api/Personas/SelectProveedores
        [Authorize(Roles = "Bodeguero,Administrador,SuperUsuario")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectProveedores>> SelectProveedores()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Proveedor").ToListAsync();

            return persona.Select(p => new SelectProveedores
            {
                Idpersona = p.Idpersona,
                Nombre = p.Nombre,

            });
        }

        // GET: api/Personas/SelectClientes
        [Authorize(Roles = "Vendedor,Administrador,SuperUsuario")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectProveedores>> SelectClientes()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Cliente").ToListAsync();

            return persona.Select(p => new SelectProveedores
            {
                Idpersona = p.Idpersona,
                Nombre = p.Nombre,

            });
        }

        // POST: api/Personas/Crear
        [Authorize(Roles = "Vendedor,Administrador,Bodeguero,SuperUsuario")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] Persona model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.Email.ToLower();

            if (await _context.Personas.AnyAsync(p => p.Email == email))
            {
                return BadRequest("El email ya existe");
            }

            Persona persona = new Persona
            {
                TipoPersona = model.TipoPersona,
                Nombre = model.Nombre,
                TipoDocumento = model.TipoDocumento,
                NumDocumento = model.NumDocumento,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email.ToLower()
            };

            _context.Personas.Add(persona);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Personas/Actualizar
        [Authorize(Roles = "Vendedor,Administrador,Bodeguero,SuperUsuario")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] Persona model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Idpersona <= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.Idpersona == model.Idpersona);

            if (persona == null)
            {
                return NotFound();
            }

            persona.TipoPersona = model.TipoPersona;
            persona.Nombre = model.Nombre;
            persona.TipoDocumento = model.TipoDocumento;
            persona.NumDocumento = model.NumDocumento;
            persona.Direccion = model.Direccion;
            persona.Telefono = model.Telefono;
            persona.Email = model.Email.ToLower();

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

        // DELETE: api/Personas/Eliminar/1
        [Authorize(Roles = "SuperUsuario")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(persona);
        }

    }

}