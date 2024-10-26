namespace BaseDeDatosconTayer
{
    public class ProductoRepository
    {
        private List<Producto> listaProductos;

        public ProductoRepository()
        {
            listaProductos = new List<Producto>();
        }

        public bool CrearProducto(Producto product, int cant)
        {
            if(product != null)
            {
                listaProductos.Add(product);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ModificarProducto(int id, Producto product)
        {
            Producto? aux = Buscar(id);
            if(aux != null)
            {
                aux = product;
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Producto> ObtenerProductos()
        {
            return listaProductos;
        }

        public bool EliminarProducto(int id)
        {
            Producto? aux = Buscar(id);
            if(aux != null)
            {
                listaProductos.Remove(aux);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Producto? Buscar(int id)
        {
            Producto? aux = null; 
            foreach(Producto prod in listaProductos)
            {
                if(prod.IdProducto == id)
                {
                    aux = prod;
                    break;
                }
            }
            return aux;
        }
    }
}