using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaErick2.Models;

namespace SistemaErick2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly BdsistemaContext _context;

        public ArticulosController(BdsistemaContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public IActionResult Listar2()
        { 
            List<Articulo> lista = new List<Articulo>();

            try { 
                lista = _context.Articulos.Include(c=>c.IdcategoriaNavigation).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = lista });
            
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }

        // GET: api/Articulos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Articulo>> Listar()
        {
            var articulo = await _context.Articulos.Include(a => a.IdcategoriaNavigation).ToListAsync();

            return articulo .Select(a => new Articulo
            {
                Idarticulo= a.Idarticulo,
                Idcategoria= a.Idcategoria,
                IdcategoriaNavigation= a.IdcategoriaNavigation,
                Codigo= a.Codigo,
                Nombre= a.Nombre,
                Stock= a.Stock,
                PrecioVenta= a.PrecioVenta,
                Descripcion= a.Descripcion,
                Condicion= a.Condicion,

            });

        }
        // GET: api/Articulos/Mostrar/1
    
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var articulo = await _context.Articulos.Include(a => a.IdcategoriaNavigation).SingleOrDefaultAsync(a => a.Idarticulo==id);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new Articulo
            {
                Idarticulo=articulo.Idarticulo,
                Idcategoria=articulo.Idcategoria,
                Codigo=articulo.Codigo,
                IdcategoriaNavigation=articulo.IdcategoriaNavigation,
                Nombre=articulo.Nombre,
                Descripcion=articulo.Descripcion,
                Stock=articulo.Stock,
                PrecioVenta=articulo.PrecioVenta,
                Condicion=articulo.Condicion,

            });
        }

        // GET: api/Articulos/ListarIngreso/texto
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<Articulo>> ListarIngreso([FromRoute] string texto)
        {
            var articulo = await _context.Articulos.Include(a => a.IdcategoriaNavigation)
                .Where(a=>a.Nombre.Contains(texto))
                .Where(a=>a.Condicion==true)
                .ToListAsync();

            return articulo.Select(a => new Articulo
            {
                Idarticulo = a.Idarticulo,
                Idcategoria = a.Idcategoria,
                IdcategoriaNavigation= a.IdcategoriaNavigation,
                Codigo = a.Codigo,
                Nombre = a.Nombre,
                Stock = a.Stock,
                PrecioVenta = a.PrecioVenta,
                Descripcion = a.Descripcion,
                Condicion = a.Condicion
            });

        }

        // GET: api/Articulos/ListarVenta/texto
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<Articulo>> ListarVenta([FromRoute] string texto)
        {
            var articulo = await _context.Articulos.Include(a => a.IdcategoriaNavigation)
                .Where(a=>a.Nombre.Contains(texto))
                .Where(a=>a.Condicion==true)
                .Where(a=>a.Stock>0)
                .ToListAsync();

            return articulo.Select(a => new Articulo
            {
                Idarticulo = a.Idarticulo,
                Idcategoria = a.Idcategoria,
                IdcategoriaNavigation= a.IdcategoriaNavigation,
                Codigo = a.Codigo,
                Nombre = a.Nombre,
                Stock = a.Stock,
                PrecioVenta = a.PrecioVenta,
                Descripcion = a.Descripcion,
                Condicion = a.Condicion
            });

        }


         // GET: api/Articulos/BuscarCodigoIngreso/1546845213
        [HttpGet("[action]/{codigo}")]
        public async Task<IActionResult> BuscarCodigoIngreso ([FromRoute] string Codigo)
        {

            var articulo = await _context.Articulos.Include(a => a.IdcategoriaNavigation).Where(a=>a.Condicion==true)
            .SingleOrDefaultAsync(a => a.Codigo == Codigo);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new Articulo
            {
                Idarticulo=articulo.Idarticulo,
                Idcategoria=articulo.Idcategoria,
                Codigo=articulo.Codigo,
                IdcategoriaNavigation=articulo.IdcategoriaNavigation,
                Nombre=articulo.Nombre,
                Descripcion=articulo.Descripcion,
                Stock=articulo.Stock,
                PrecioVenta=articulo.PrecioVenta,
                Condicion=articulo.Condicion,

            });
        }

         // GET: api/Articulos/BuscarCodigoVenta/1546845213
        [HttpGet("[action]/{codigo}")]
        public async Task<IActionResult> BuscarCodigoVenta ([FromRoute] string Codigo)
        {

            var articulo = await _context.Articulos.Include(a => a.IdcategoriaNavigation)
            .Where(a=>a.Condicion==true)
            .Where(a=>a.Stock>0)
            .SingleOrDefaultAsync(a => a.Codigo == Codigo);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new Articulo
            {
                Idarticulo=articulo.Idarticulo,
                Idcategoria=articulo.Idcategoria,
                Codigo=articulo.Codigo,
                IdcategoriaNavigation=articulo.IdcategoriaNavigation,
                Nombre=articulo.Nombre,
                Descripcion=articulo.Descripcion,
                Stock=articulo.Stock,
                PrecioVenta=articulo.PrecioVenta,
                Condicion=articulo.Condicion,

            });
        }

        // PUT: api/Articulos/Editar
    
        [HttpPut("[action]")]
        public IActionResult Editar([FromBody] Articulo model)
        {
            Articulo articulo = _context.Articulos.Find(model.Idarticulo);

            if (articulo == null)
            { 
                return BadRequest("Articulo No Encontrado");
            }

            try
            {
                articulo.Codigo = model.Codigo is null ? articulo.Codigo : articulo.Codigo;
                articulo.Nombre = model.Nombre is null ? articulo.Nombre : articulo.Nombre;
                articulo.PrecioVenta = model.PrecioVenta is null ? articulo.PrecioVenta : articulo.PrecioVenta;
                articulo.Stock = model.Stock is null ? articulo.Stock : articulo.Stock;
                articulo.Descripcion = model.Descripcion is null ? articulo.Descripcion : articulo.Descripcion;
                articulo.Condicion = model.Condicion is null ? articulo.Condicion : articulo.Condicion;
                articulo.Idcategoria = model.Idcategoria is null ? articulo.Idcategoria : articulo.Idcategoria;


                articulo.Idcategoria = model.Idcategoria;
                articulo.Codigo = model.Codigo;
                articulo.Nombre = model.Nombre;
                articulo.PrecioVenta = model.PrecioVenta;
                articulo.Stock = model.Stock;
                articulo.Descripcion = model.Descripcion;

                _context.Articulos.Update(articulo); 
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }


        }

        // POST: api/Articulos/Crear
    
        [HttpPost("[action]")]
        public IActionResult Crear([FromBody] Articulo model)
        {
            try
            {
                _context.Articulos.Add(model);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});

            }

        }


        // PUT: api/Articulos/Actualizar
    
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] Articulo model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Idarticulo <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Idarticulo == model.Idarticulo);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Idcategoria= model.Idcategoria;
            articulo.Codigo= model.Codigo;
            articulo.Nombre= model.Nombre;
            articulo.PrecioVenta= model.PrecioVenta;
            articulo.Stock = model.Stock;
            articulo.Descripcion= model.Descripcion;

            
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


        // PUT: api/Articulos/Activar/1
    
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Condicion = true;

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

        // PUT: api/Articulos/Desactivar/1 
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Condicion = false;

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

        // DELETE: api/Articulos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }

            _context.Articulos.Remove(articulo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(articulo);
        }

        
    }
}
