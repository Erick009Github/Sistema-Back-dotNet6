using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaErick2.Models;

namespace SistemaErick2.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class VentasController : ControllerBase
    {

        private readonly BdsistemaContext _context;

        public VentasController(BdsistemaContext context)
        {
            _context = context;
      
        }
    
        // GET: api/Ventas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<VentaList>> Listar()
        {
            var venta = await _context.Venta
                .Include(v => v.IdusuarioNavigation)
                .Include(v => v.IdclienteNavigation)
                .OrderByDescending(v => v.Idventa)
                .Take(100)
                .ToListAsync();

            return venta.Select(v => new VentaList            {
                Idventa = v.Idventa,
                Idcliente = v.Idcliente,
                IdclienteNavigation = v.IdclienteNavigation,
                Idusuario = v.Idusuario,
                IdusuarioNavigation=v.IdusuarioNavigation,
                TipoComprobante = v.TipoComprobante,
                SerieComprobante = v.SerieComprobante,
                NumComprobante = v.NumComprobante,
                FechaHora = v.FechaHora,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado
            });

        }

          // GET: api/Ventas/VentasMes12
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConsultaVentas>> VentasMes12()
        {
            var consulta = await _context.Venta
                .GroupBy(v => v.FechaHora.Month)
                .Select(x=> new { Etiqueta=x.Key, Valor =x.Sum(v=>v.Total)})
                .OrderByDescending(x => x.Etiqueta)
                .Take(12)
                .ToListAsync();

            return consulta.Select(v => new ConsultaVentas{
                Etiqueta= v.Etiqueta.ToString(),
                Valor = v.Valor
            });

        }


         // GET: api/Ventas/ListarFiltro/texto
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<VentaList>> ListarFiltro([FromRoute] string texto)
        {
            var venta = await _context.Venta
                .Include(v => v.IdusuarioNavigation)
                .Include(v => v.IdclienteNavigation)
                .Where(v => v.NumComprobante.Contains(texto))
                .OrderByDescending(v => v.Idventa)
                .ToListAsync();

            return venta.Select(v => new VentaList
            {
                
                Idventa = v.Idventa,
                Idcliente = v.Idcliente,
                IdclienteNavigation = v.IdclienteNavigation,
                Idusuario = v.Idusuario,
                IdusuarioNavigation=v.IdusuarioNavigation,
                TipoComprobante = v.TipoComprobante,
                SerieComprobante = v.SerieComprobante,
                NumComprobante = v.NumComprobante,
                FechaHora = v.FechaHora,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado

            });

         }

        // GET: api/Ventas/ConsultaFechas
        [HttpGet("[action]/{FechaInicio}/{FechaFin}")]
        public async Task<IEnumerable<VentaList>> ConsultaFechas([FromRoute]DateTime FechaInicio,DateTime FechaFin)
        {
            var venta = await _context.Venta
                .Include(v => v.IdusuarioNavigation)
                .Include(v => v.IdclienteNavigation)
                .Where(i => i.FechaHora>=FechaInicio)
                .Where(i => i.FechaHora<=FechaFin) 
                .OrderByDescending(v => v.Idventa)
                .Take(100)
                .ToListAsync();

            return venta.Select(v => new VentaList{
                Idventa = v.Idventa,
                Idcliente = v.Idcliente,
                IdclienteNavigation = v.IdclienteNavigation,
                Idusuario = v.Idusuario,
                IdusuarioNavigation=v.IdusuarioNavigation,
                TipoComprobante = v.TipoComprobante,
                SerieComprobante = v.SerieComprobante,
                NumComprobante = v.NumComprobante,
                FechaHora = v.FechaHora,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado
            });

        }

        // GET: api/Ventas/ListarDetalles
        [HttpGet("[action]/{Idventa}")]
        public async Task<IEnumerable<DetalleVentum>> ListarDetalles([FromRoute] int Idventa)
        {
            var detalle = await _context.DetalleVenta
                .Include(a => a.IdarticuloNavigation)
                .Where(d => d.Idventa==Idventa)
                .ToListAsync();

            return detalle.Select(d => new DetalleVentum
            {
                Idarticulo = d.Idarticulo,
                IdarticuloNavigation = d.IdarticuloNavigation,
                Cantidad = d.Cantidad,
                Precio = d.Precio,
                Descuento=d.Descuento
            });

        }

        // POST: api/Ventas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearVenta model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            Ventum venta = new Ventum
            {
                Idcliente = model.Idcliente,
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
                _context.Venta.Add(venta);
                await _context.SaveChangesAsync();

                var id = venta.Idventa;
                foreach (var det in model.Detalles)
                {
                    DetalleVentum detalle = new DetalleVentum
                    {
                        Idventa = id,
                        Idarticulo = det.Idarticulo,
                        Cantidad = det.Cantidad,
                        Precio = det.Precio,
                        Descuento=det.Descuento
                    };
                    _context.DetalleVenta.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Ventas/Anular/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var venta = await _context.Venta.FirstOrDefaultAsync(v => v.Idventa == id);

            if (venta == null)
            {
                return NotFound();
            }

            venta.Estado = "Anulado";

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

    }


}