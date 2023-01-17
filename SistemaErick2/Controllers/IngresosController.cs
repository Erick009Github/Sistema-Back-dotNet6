using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaErick2.Models;

namespace SistemaErick2.Controllers
{

    [Route("api/[controller]")]
    [ApiController] 
    public class IngresosController : ControllerBase
    {

        private readonly BdsistemaContext _context;

        public IngresosController(BdsistemaContext context)
        {
            _context = context;
        }

          // GET: api/Ingresos/Listar
        
        [HttpGet("[action]")]
        public async Task<IEnumerable<Ingreso>> Listar()
        {
            var ingreso = await _context.Ingresos
            .Include(i => i.IdusuarioNavigation)
            .Include(i =>i.IdproveedorNavigation)
            .OrderByDescending(i=>i.Idingreso)
            .Take(100)
            .ToListAsync();

            return ingreso .Select(i => new Ingreso
            {
            Idingreso=i.Idingreso,
            Idproveedor=i.Idproveedor,
            IdproveedorNavigation=i.IdproveedorNavigation,
            Idusuario=i.Idusuario,
            IdusuarioNavigation=i.IdusuarioNavigation,
            TipoComprobante=i.TipoComprobante,
            NumComprobante=i.NumComprobante,
            SerieComprobante=i.SerieComprobante,
            FechaHora=i.FechaHora,
            Impuesto=i.Impuesto,
            Total=i.Total,
            Estado=i.Estado
            });
        }

         // GET: api/Ingresos/ConsultaFechas
        [HttpGet("[action]/{FechaInicio}/{FechaFin}")]
        public async Task<IEnumerable<Ingreso>> ConsultaFechas([FromRoute]DateTime FechaInicio,DateTime FechaFin)
        {
            var ingreso = await _context.Ingresos
                .Include(v => v.IdusuarioNavigation)
                .Include(v => v.IdproveedorNavigation)
                .Where(i => i.FechaHora>=FechaInicio)
                .Where(i => i.FechaHora<=FechaFin)
                .OrderByDescending(v => v.Idingreso)
                .Take(100)
                .ToListAsync();

            return ingreso.Select(i => new Ingreso{
                Idingreso = i.Idingreso,
                Idproveedor=i.Idproveedor,
                IdproveedorNavigation=i.IdproveedorNavigation,
                Idusuario = i.Idusuario,
                IdusuarioNavigation=i.IdusuarioNavigation,
                TipoComprobante=i.TipoComprobante,
                SerieComprobante = i.SerieComprobante,
                NumComprobante = i.NumComprobante,
                FechaHora = i.FechaHora,
                Impuesto = i.Impuesto,
                Total = i.Total,
                Estado = i.Estado
            });

        }

         // GET: api/Ingresos/ListarDetalles
        [HttpGet("[action]/{idingreso}")]
        public async Task<IEnumerable<DetalleIngreso>> ListarDetalles([FromRoute] int Idingreso)
        {
            var detalle = await _context.DetalleIngresos
                .Include(a => a.IdarticuloNavigation)
                .Where(d=>d.Idingreso==Idingreso)
                .ToListAsync();

            return detalle.Select(d => new DetalleIngreso
            {
                Idarticulo=d.Idarticulo,
                IdarticuloNavigation=d.IdarticuloNavigation,
                Cantidad=d.Cantidad,
                Precio=d.Precio
            });

        }

        // POST: api/Ingresos/Crear
    
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearIngreso model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            Ingreso ingreso = new Ingreso {
                
                Idproveedor = model.Idproveedor,
                Idusuario = model.Idusuario,
                TipoComprobante = model.TipoComprobante,
                SerieComprobante = model.SerieComprobante,
                NumComprobante = model.NumComprobante,
                FechaHora = fechaHora,
                Impuesto = model.Impuesto,
                Total = model.Total,
                Estado = "Aceptado"
            };      

            try
            {
                _context.Ingresos.Add(ingreso);
                await _context.SaveChangesAsync();

                var id = ingreso.Idingreso;
                foreach (var det in model.Detalles)
                {
                    DetalleIngreso detalle = new DetalleIngreso
                    {
                        Idingreso = id,
                        Idarticulo = det.Idarticulo,
                        Cantidad = det.Cantidad,
                        Precio = det.Precio
                    };
                    _context.DetalleIngresos.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

         // PUT: api/Ingresos/Anular/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var ingreso = await _context.Ingresos.FirstOrDefaultAsync(i=>i.Idingreso == id);

            if (ingreso == null)
            {
                return NotFound();
            }

            ingreso.Estado = "Anulado";

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

        // DELETE: api/Ingresos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso == null)
            {
                return NotFound();
            }

            _context.Ingresos.Remove(ingreso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(ingreso);
        }

    }

}