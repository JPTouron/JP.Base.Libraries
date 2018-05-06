using JP.Base.DAL.ADO.ConnectionManagement;

namespace JP.Base.DAL.ADO.TransactionManagement
{
    public interface ITransactionManager
    {
        void Finalizar_Transaccion(string IDTransaccion, bool pDeshacerTransaccion);

        void Finalizar_Transaccion(string IDTransaccion);

        string Iniciar_Transaccion(out IDBAdoConnection DBConnTransaccionada);

        IDBAdoConnection Obtener_Conexion_De_Transaccion(string pIdTransaccion);

        IDBAdoConnection Obtener_Conexion_De_Transaccion();
    }
}