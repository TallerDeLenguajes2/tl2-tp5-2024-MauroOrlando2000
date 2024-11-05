namespace BaseDeDatosconTayer
{
    public interface IPresupuestoRepository
    {
        List<Presupuesto> ObtenerPresupuestos();
        bool CrearPresupuesto(Presupuesto budget);
        Presupuesto? Buscar(int id);
        bool AgregarProducto(int idPres, int idProd, int cant);
        bool EliminarPresupuesto(int id);
    }
}