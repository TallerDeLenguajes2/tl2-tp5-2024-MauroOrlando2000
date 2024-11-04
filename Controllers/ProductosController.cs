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
            if(!repositorioProductos.CrearProducto(product))
            {
                return NotFound("Producto no creado");
            }
            return Created($"api/productos/{product.IdProducto}", product);
        }

        [HttpGet("ObtenerProductos")]
        public IActionResult ObtenerProductos()
        {
            if(repositorioProductos.ObtenerProductos().Count == 0)
            {
                return NoContent();
            }
            return Ok(repositorioProductos.ObtenerProductos());
        }

        [HttpGet("ObtenerProductoPorID/{id}")]
        public IActionResult ObtenerProductoPorID(int id)
        {
            if(repositorioProductos.Buscar(id) == null || repositorioProductos.Buscar(id) == default(Producto))
            {
                return NotFound("No se encontro. Revise los datos ingresados");
            }
            return Ok(repositorioProductos.Buscar(id));
        }

        [HttpPut("ModificarProducto/{id}")]
        public IActionResult ModificarProducto(int id, Producto product)
        {
            if(!repositorioProductos.ModificarProducto(id, product))
            {
                return NotFound("No se encontro. Revise los datos ingresados");
            }
            return Ok("Producto modificado exitosamente");
        }

        [HttpDelete("EliminarProducto/{id}")]
        public IActionResult EliminarProducto(int id)
        {
            if(!repositorioProductos.EliminarProducto(id))
            {
                return NotFound("Producto no eliminado. Revise los datos ingresados");
            }
            return Ok($"Producto {id} eliminado");
        }
    }
}