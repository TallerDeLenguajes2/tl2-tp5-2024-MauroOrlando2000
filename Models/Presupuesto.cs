using System.ComponentModel.DataAnnotations.Schema;

namespace BaseDeDatosconTayer
{
    public class Presupuesto
    {
        private int idPresupuesto;
        private string nombreDestinatario;
        private string fechaCreacion;
        private List<PresupuestoDetalle> detalle;

        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
        public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
        public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public List<PresupuestoDetalle> Detalle { get => detalle; }

        public Presupuesto(){}

        public Presupuesto(int id, string nom, string fecha, List<PresupuestoDetalle> lista)
        {
            idPresupuesto = id;
            nombreDestinatario = nom;
            fechaCreacion = fecha;
            detalle = lista;
        }

        public double MontoPresupuesto()
        {
            double monto = 0;
            foreach(PresupuestoDetalle detail in detalle)
            {
                monto += detail.Cantidad * detail.ObtenerProducto().Precio;
            }
            return monto;
        }

        public double MontoPresupuestoConIVA()
        {
            double monto = MontoPresupuesto() * 1.21;
            return monto;
        }

        public int CantidadProductos()
        {
            int cantidad = 0;
            foreach(PresupuestoDetalle detail in detalle)
            {
                cantidad += cantidad;
            }
            return cantidad;
        }

        public void AgregarProducto(Producto product, int cant)
        {
            PresupuestoDetalle nuevo = new PresupuestoDetalle(product, cant);
            detalle.Add(nuevo);
        }
    }
}