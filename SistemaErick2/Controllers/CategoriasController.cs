using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaErick2.Models;

namespace SistemaErick2.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController] 
    public class CategoriasController : ControllerBase
    {

        private readonly BdsistemaContext _context;

        public CategoriasController(BdsistemaContext context)
        {
            _context = context;
        }

        // GET: api/Categorias/Listar
   
        [HttpGet("[action]")]
        public async Task<IEnumerable<Categorium>> Listar()
        {
            var categoria = await _context.Categoria.ToListAsync();

            return categoria.Select(c => new Categorium
            {
                Idcategoria = c.Idcategoria,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Condicion = c.Condicion
            });

        }

        
        // GET: api/Categorias/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectCategoria>> Select()
        {
            var categoria = await _context.Categoria.Where(c=>c.Condicion==true).ToListAsync();

            return categoria.Select(c => new SelectCategoria
            {
                Idcategoria = c.Idcategoria,
                Nombre = c.Nombre,
               
            });

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var categoria = await _context.Categoria.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new Categorium
            {
                Idcategoria = categoria.Idcategoria,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Condicion = categoria.Condicion
            });
        }

        // PUT: api/Categorias/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] Categorium model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Idcategoria <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Idcategoria == model.Idcategoria);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Nombre = model.Nombre;
            categoria.Descripcion = model.Descripcion;

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

        // POST: api/Categorias/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] Categorium model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Categorium categoria = new Categorium
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Condicion = true
            };

            _context.Categoria.Add(categoria);
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

        // DELETE: api/Categorias/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(categoria);
        }

        // PUT: api/Categorias/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Idcategoria == id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Condicion = false;

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

        // PUT: api/Categorias/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Idcategoria == id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Condicion = true;

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

        private bool CategoriasExists(int id)
        {
            return _context.Categoria.Any(e => e.Idcategoria == id);
        }
    }
}


