namespace BaseDeDatosconTayer
{
    public interface IPresupuestoRepository
    {
        IEnumerable<Presupuesto> ObtenerPresupuestos();
        bool CrearPresupuesto(Presupuesto budget);
        /* Presupuesto? Buscar(int id);
        bool AgregarProductoYCantidad(int id);
        bool EliminarPresupuesto(int id); */
    }
}