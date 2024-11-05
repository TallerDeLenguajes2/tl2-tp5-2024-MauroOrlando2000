using System.ComponentModel.DataAnnotations.Schema;

namespace BaseDeDatosconTayer
{
    public class Presupuesto
    {
        private int idPresupuesto;
        private string nombreDestinatario;
        private string fechaCreacion;
        private List<PresupuestoDetalle> detalle;

        public int IdPresupuesto { get => idPresupuesto; }
        public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
        public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }

        public Presupuesto(){}

        public Presupuesto(int id, string nom, string fecha)
        {
            idPresupuesto = id;
            nombreDestinatario = nom;
            fechaCreacion = fecha;
            detalle = new List<PresupuestoDetalle>();
        }

        public double MontoPresupuesto()
        {
            double monto = 0;
            foreach(PresupuestoDetalle detail in detalle)
            {
                monto += detail.Cantidad * detail.Producto.IdProducto;
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

        public bool AgregarProducto(Producto product, int cant)
        {
            PresupuestoDetalle nuevo = new PresupuestoDetalle(product, cant);
            detalle.Add(nuevo);
            return true;
        }

        public void CambiarID(int id)
        {
            idPresupuesto = id;
        }
    }
}