namespace JP.Base.DAL.ADO.Contracts
{
    public interface ITransactionManager
    {
        void Finalizar_Transaccion(string IDTransaccion, bool pDeshacerTransaccion);

        void Finalizar_Transaccion(string IDTransaccion);

        string Iniciar_Transaccion(out IDbAdoConnection DBConnTransaccionada);

        IDbAdoConnection Obtener_Conexion_De_Transaccion(string pIdTransaccion);

        IDbAdoConnection Obtener_Conexion_De_Transaccion();
    }
}