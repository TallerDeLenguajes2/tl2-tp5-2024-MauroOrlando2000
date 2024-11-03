using Microsoft.AspNetCore.Mvc;
namespace BaseDeDatosconTayer
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private IProductoRepository repositorioProductos;

        public ProductosController()
        {
            repositorioProductos = new ProductoRepository();
        }

        [HttpPost("CrearProducto")]
        public IActionResult CrearProducto(Producto product)
        {
            if(repositorioProductos.CrearProducto(product))
            {
                return Created(product);
            }
            else
            {
                return Err
            }
        }

        [HttpGet("ObtenerProductos")]
        public IActionResult ObtenerProductos()
        {
            if(!repositorioProductos.ObtenerProductos().Any())
            {
                return NoContent();
            }
            return Ok(repositorioProductos.ObtenerProductos());
        }

        [HttpGet("ObtenerProductoPorID/{id}")]
        public IActionResult ObtenerProductoPorID(int id)
        {
            if(!repositorioProductos.ObtenerProductos().Any())
            {
                return NotFound("No hay productos");
            }
            else
            {
                if(repositorioProductos.Buscar(id) == null)
                {
                    return NotFound("No se encontro. Revise los datos ingresados");
                }
                return Ok(repositorioProductos.Buscar(id));
            }
        }
    }
}