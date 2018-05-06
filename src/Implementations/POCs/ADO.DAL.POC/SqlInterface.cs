using JP.Base.DAL.ADO.ConnectionManagement;
using System.Data;

namespace ADO.DAL.POC
{
    internal class SqlInterface
    {

        public void GetData()
        {
            DataTable dt;

            using (var conn =SetConnection())
            {

                conn.Abrir_Conexion();
                conn.Crear_Comando("select * from Clients", CommandType.Text);

                dt = conn.Ejecutar_Comando_De_Retorno_Tabular();

            }



        }

        private IDBAdoConnection SetConnection()
        {
            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CasiraghiDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

            var connstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CasiraghiDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            return DBConnFactory.Obtener_Nueva_Conexion("System.Data.SqlClient", connstring);
        }
    }
}