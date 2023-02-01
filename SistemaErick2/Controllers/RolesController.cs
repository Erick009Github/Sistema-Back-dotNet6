using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaErick2.Models;

namespace SistemaErick2.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly BdsistemaContext _context;

        public RolesController(BdsistemaContext context)
        {
            _context = context;
        }

      // GET: api/Roles/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<Rol>> Listar()
        {
            var rol = await _context.Rols.ToListAsync();

            return rol.Select(r => new Rol
            {
                Idrol = r.Idrol,
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Condicion = r.Condicion
            });

        }

        
        // GET: api/Roles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectRol>> Select()
        {
            var rol = await _context.Rols.Where(r=>r.Condicion==true).ToListAsync();

            return rol.Select(r => new SelectRol
            {
                Idrol = r.Idrol,
                Nombre = r.Nombre,
            
            });

        }

    }

}

