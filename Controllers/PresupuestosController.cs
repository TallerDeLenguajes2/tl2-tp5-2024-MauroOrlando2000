using Microsoft.AspNetCore.Mvc;
namespace BaseDeDatosconTayer
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestosController : ControllerBase
    {
        private IPresupuestoRepository repositorioPresupuestos;

        public PresupuestosController()
        {
            repositorioPresupuestos = new PresupuestoRepository();
        }

        [HttpPost("CrearPrespuesto")]
        public IActionResult CrearPresupuesto(Presupuesto presupuesto)
        {
            if(!repositorioPresupuestos.CrearPresupuesto(presupuesto))
            {
                return NotFound("Presupuesto no creado. Revise lo datos ingresados");
            }
            return Created($"api/presupuestos/{presupuesto.IdPresupuesto}", presupuesto);
        }

        [HttpGet("ObtenerPresupuestos")]
        public IActionResult ObtenerPresupuestos()
        {
            if(repositorioPresupuestos.ObtenerPresupuestos().Count == 0)
            {
                return NoContent();
            }
            return Ok(repositorioPresupuestos.ObtenerPresupuestos());
        }

        [HttpGet("ObtenerPresupuestos/{id}")]
        public IActionResult ObtenerPresupuestoPorID(int id)
        {
            if(repositorioPresupuestos.Buscar(id) == null || repositorioPresupuestos.Buscar(id) == default(Presupuesto))
            {
                return NotFound("No se encontro. Revise los datos ingresados");
            }
            return Ok(repositorioPresupuestos.Buscar(id));
        }
        
        [HttpPut("AgregarProducto/{idPres}")]
        public IActionResult AgregarProducto(int idPres,[FromBody] int idProd, int cant)
        {
            if(!repositorioPresupuestos.AgregarProducto(idPres, idProd, cant))
            {
                return NotFound("No se encontro el presupuesto o el producto. Revise los datos ingresados");
            }
            return Ok("Producto agregado al presupuesto exitosamente");
        }

        [HttpDelete("EliminarPresupuesto/{id}")]
        public IActionResult EliminarPresupuesto(int id)
        {
            if(!repositorioPresupuestos.EliminarPresupuesto(id))
            {
                return NotFound("Presupuesto no elimiando. Revise los datos ingresados");
            }
            return Ok($"Presupuesto {id} Eliminado");
        }
    }
}