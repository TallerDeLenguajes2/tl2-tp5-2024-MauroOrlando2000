using Microsoft.Data.Sqlite;

namespace BaseDeDatosconTayer
{
    public class ProductoRepository : IProductoRepository
    {
        string cadenaConexion = "Data Source=DB/Tienda.db;Cache=Shared;";
        private readonly List<Producto> listaProductos = new List<Producto>();

        public bool CrearProducto(Producto product)
        {
            bool anda = false;
            if(product != null)
            {
                using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    List<Producto> productos = new List<Producto>();
                    var query = "SELECT * FROM Productos;";
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    command.Connection.Open();
                    using(var DataReader = command.ExecuteReader())
                    {
                        while(DataReader.Read())
                        {
                            int id = Convert.ToInt16(DataReader["idProducto"]);
                            string desc = Convert.ToString(DataReader["Descripcion"]);
                            int price = Convert.ToInt16(DataReader["Precio"]);
                            Producto nuevo = new Producto(id, desc, price);
                            productos.Add(nuevo);
                        }
                    }
                    command.Connection.Close();
                    if(!productos.Contains(product))
                    {
                        query = $"INSERT INTO Productos (idProducto, Descripcion, Precio) VALUES ({product.IdProducto}, {product.Descripcion}, {product.Precio});";
                        connection.Open();
                        command = new SqliteCommand(query, connection);
                        command.Connection.Open();
                        anda = command.ExecuteNonQuery() > 0;
                        command.Connection.Close();
                    }
                    connection.Close();
                }
            }
            return anda;
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

        public IEnumerable<Producto> ObtenerProductos()
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