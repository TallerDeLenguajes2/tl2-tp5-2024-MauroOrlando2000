using System.ComponentModel.DataAnnotations.Schema;

namespace BaseDeDatosconTayer
{
    public class Presupuesto
    {
        private int idPresupuesto;
        private string nombreDestinatario;
        private List<PresupuestoDetalle> detalle;

        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
        public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }

        public Presupuesto(){}

        public Presupuesto(int id, string nom, List<PresupuestoDetalle> lista)
        {
            idPresupuesto = id;
            nombreDestinatario = nom;
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
    }
}