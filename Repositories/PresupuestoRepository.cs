using Microsoft.Data.Sqlite;

namespace BaseDeDatosconTayer
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        string CadenaDeConexion = "Data Source=DB/Tienda.db;Cache=Shared";

        public bool CrearPresupuesto(Presupuesto budget)
        {
            if(budget != null)
            {
                using(SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
                {
                    connection.Open();
                    var query = $"INSERT INTO Presupuestos (idPresupuesto, NombreDestinatario FechaCreacion) VALUES ({budget.IdPresupuesto}, @NombreDestinatario, @FechaCreacion);";
                    var command = new SqliteCommand(query, connection);
                    command.Connection.Open();
                    int safe = command.ExecuteNonQuery();
                    if(safe > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Presupuesto> ObtenerPresupuestos()
        {
            List<Presupuesto> lista = new List<Presupuesto>();
            using(SqliteConnection connection = new SqliteConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Presupuestos LEFT JOIN PresupuestosDetalle USING(idPresupuesto) LEFT JOIN Productos USING(idProducto)";
                var command = new SqliteCommand(query, connection);
                command.Connection.Open();
                using(var DataReader = command.ExecuteReader())
                {
                    while(DataReader.Read())
                    {
                        int idPres = Convert.ToInt16(DataReader["idPresupuesto"]);
                        if(!lista.Exists(x => x.IdPresupuesto == idPres))
                        {
                            string nombre = Convert.ToString(DataReader["NombreDestinatario"]);
                            string fecha = Convert.ToString(DataReader["FechaCreacion"]);
                            Presupuesto nuevoPres = new Presupuesto(idPres, nombre, fecha, new List<PresupuestoDetalle>());
                            lista.Add(nuevoPres);
                        }
                        if(Convert.ToString(DataReader["Descripcion"]) != null)
                        {
                            int idProd = Convert.ToInt16(DataReader["idProducto"]);
                            string desc = Convert.ToString(DataReader["Descripcion"]);
                            int price = Convert.ToInt16(DataReader["Precio"]);
                            Producto nuevo = new Producto(idProd, desc, price);
                            int cant = Convert.ToInt16(DataReader["Cantidad"]);
                            lista.Find(x => x.IdPresupuesto == idPres).AgregarProducto(nuevo, cant);
                        }
                    }
                }
            }
            return lista;
        }

        /* public bool AgregarProductoYCantidad(int id)
        {
            Presupuesto? aux = Buscar(id);
            if(aux != null)
            {
                int contador = 0, precio = 3000, cant = 5;
                string? desc = "nuevo Producto";
                foreach(Presupuesto pres in listaPresupuestos)
                {
                    foreach(PresupuestoDetalle detail in pres.Detalle)
                    {
                        if(detail.ObtenerProducto().IdProducto > contador)
                        {
                            contador = detail.ObtenerProducto().IdProducto;
                        }
                    }
                }
                Producto nuevo = new Producto(++contador, desc, precio);
                aux.AgregarProducto(nuevo, cant);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarPresupuesto(int id)
        {
            Presupuesto? aux = Buscar(id);
            if(aux != null)
            {
                listaPresupuestos.Remove(aux);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Presupuesto? Buscar(int id)
        {
            Presupuesto? aux = null; 
            foreach(Presupuesto pres in listaPresupuestos)
            {
                if(pres.IdPresupuesto == id)
                {
                    aux = pres;
                    break;
                }
            }
            return aux;
        } */
    }
}