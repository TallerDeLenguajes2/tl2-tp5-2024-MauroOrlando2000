using Microsoft.Data.Sqlite;

namespace BaseDeDatosconTayer
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        string cadenaConexion = "Data Source=DB/Tienda.db;Cache=Shared";

        public bool CrearPresupuesto(Presupuesto budget)
        {
            bool anda = false;
            if(budget != null)
            {
                using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    List<Presupuesto> listaPresupuestos = ObtenerPresupuestos();
                    int contador = 1;
                    foreach(Presupuesto presupuesto in listaPresupuestos)
                    {
                        if(presupuesto.IdPresupuesto > contador)
                        {
                            contador = presupuesto.IdPresupuesto;
                        }
                    }
                    budget.CambiarID(++contador);
                    connection.Open();
                    var query = $"INSERT INTO Presupuestos (idPresupuesto, NombreDestinatario FechaCreacion) VALUES ({budget.IdPresupuesto}, '{budget.NombreDestinatario}', '{budget.FechaCreacion}');";
                    var command = new SqliteCommand(query, connection);
                    anda = command.ExecuteNonQuery() > 0;
                    connection.Close();
                }
            }
            return anda;
        }

        public List<Presupuesto> ObtenerPresupuestos()
        {
            List<Presupuesto> lista = new List<Presupuesto>();
            using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                var query = "SELECT * FROM Presupuestos";
                connection.Open();
                var command = new SqliteCommand(query, connection);
                using(var DataReader = command.ExecuteReader())
                {
                    while(DataReader.Read())
                    {
                        int idPres = Convert.ToInt32(DataReader["idPresupuesto"]);
                        string nombre = Convert.ToString(DataReader["NombreDestinatario"]);
                        string fecha = Convert.ToString(DataReader["FechaCreacion"]);
                        Presupuesto nuevoPres = new Presupuesto(idPres, nombre, fecha);
                        lista.Add(nuevoPres);
                    }
                }
                query = "SELECT * FROM PresupuestosDetalle;";
                command = new SqliteCommand(query, connection);
                using(var DataReader = command.ExecuteReader())
                {
                    List<Producto> listaProductos = new ProductoRepository().ObtenerProductos();
                    while(DataReader.Read())
                    {
                        int idPresDet = Convert.ToInt32(DataReader["idPresupuesto"]);
                        int idProd = Convert.ToInt32(DataReader["idProducto"]);
                        Producto aux = listaProductos.Find(x => x.IdProducto == idProd);
                        int cant = Convert.ToInt32(DataReader["Cantidad"]);
                        lista.Find(x => x.IdPresupuesto == idPresDet).AgregarProducto(aux, cant);
                    }
                }
                connection.Close();
            }
            return lista;
        }

        public Presupuesto? Buscar(int id)
        {
            return ObtenerPresupuestos().Find(x => x.IdPresupuesto == id);
        }

        public bool AgregarProducto(int idPres, int idProd, int cant)
        {
            Presupuesto? aux = Buscar(idPres);
            Producto? auxProd = new ProductoRepository().ObtenerProductos().Find(x => x.IdProducto == idProd);
            bool anda = false;
            if(aux != null && aux != default(Presupuesto) && auxProd != null && auxProd != default(Producto))
            {
                using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    var query = $"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES ({idPres}, {idProd}, {cant});";
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    anda = command.ExecuteNonQuery() > 0;
                    connection.Close();
                }
            }
            return anda;
        }

        public bool EliminarPresupuesto(int id)
        {
            Presupuesto? aux = Buscar(id);
            bool anda = false;
            if(aux != null && aux != default(Presupuesto))
            {
                using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    var query = $"DELETE FROM Presupuestos WHERE idPresupuesto = {id};";
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    anda = command.ExecuteNonQuery() > 0;
                    connection.Close();
                }
            }
            return anda;
        }
    }
}