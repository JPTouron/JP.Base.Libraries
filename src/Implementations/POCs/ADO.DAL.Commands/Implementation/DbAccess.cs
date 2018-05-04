using ADO.DAL.Commands.Contracts;
using JP.Base.DAL.ADO.ConnectionManagement;

namespace ADO.DAL.Commands
{
    public class DbAccess : IDbAccess
    {
        public bool CreateUser(object user)
        {
            using (var c = JP.Base.DAL.ADO.ConnectionManagement.DBConnFactory.Obtener_Nueva_Conexion())
            {
                c.Abrir_Conexion();

                
                c.Crear_Comando("", System.Data.CommandType.Text);
                c.Agregar_Parametro_A_Comando(new ParameterData("", 1, System.Data.ParameterDirection.Input, System.Data.DbType.Int32));
                var dt = c.Ejecutar_Comando_De_Retorno_Tabular();


                
            }


            return true;
        }
    }
}