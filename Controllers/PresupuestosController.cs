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
            if(presupuesto == null)
            {
                return NotFound("Revise lo datos ingresados");
            }
            return Ok(repositorioPresupuestos.CrearPresupuesto(presupuesto));
        }

        /* [HttpPost("AgregarProducto/{id}/ProductoDetalle")]
        public IActionResult AgregarProducto(int id)
        {
            if(!repositorioPresupuestos.AgregarProductoYCantidad(id))
            {
                return NotFound("No se encontro. Revise los datos ingresados");
            }
            return Ok("Producto agregado exitosamente");
        } */

        [HttpGet("ObtenerPresupuestos")]
        public IActionResult ObtenerPresupuestos()
        {
            if(!repositorioPresupuestos.ObtenerPresupuestos().Any())
            {
                return NoContent();
            }
            return Ok(repositorioPresupuestos.ObtenerPresupuestos());
        }

        /* [HttpGet("ObtenerPresupuestos/{id}")]
        public IActionResult ObtenerPresupuestoPorID(int id)
        {
            if(!repositorioPresupuestos.ObtenerPresupuestos().Any())
            {
                return NotFound("No hay presupuestos");
            }
            else
            {
                if(repositorioPresupuestos.Buscar(id) == null)
                {
                    return NotFound("No se encontro. Revise los datos ingresados");
                }
                return Ok(repositorioPresupuestos.Buscar(id));
            }
        } */
    }
}