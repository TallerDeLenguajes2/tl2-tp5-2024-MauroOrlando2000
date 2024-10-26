namespace BaseDeDatosconTayer
{
    public class Producto
    {
        private int idProducto;
        private string descripcion;
        private int precio;

        public int IdProducto { get => idProducto; set => idProducto = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Precio { get => precio; set => precio = value; }

        public Producto(){}

        public Producto(int id, string des, int price)
        {
            idProducto = id;
            descripcion = des;
            precio = price;
        }
    }
}