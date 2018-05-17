using JP.Base.DAL.ADO.ConnectionManagement;
using System;
using System.Data;

namespace ADO.DAL.POC
{
    public class AccessInterface
    {
        public void GetData()
        {
            DataTable dt;

            using (var conn = SetConnection())
            {
                conn.Abrir_Conexion();
                conn.Crear_Comando("select * from Clients", CommandType.Text);

                dt = conn.Ejecutar_Comando_De_Retorno_Tabular();
            }
        }

        public void SetData()
        {
            var rnd = new Random();
            var data = rnd.Next().ToString().Substring(0, 7).PadLeft(8, '0');
            DataTable dt;

            using (var conn = SetConnection())
            {
                conn.Abrir_Conexion();
                conn.Crear_Comando($"INSERT INTO Clients (Code,Name) VALUES('{data}','Name{data}')", CommandType.Text);

                conn.Ejecutar_Comando_Sin_Retorno();

                conn.Crear_Comando($"select * from Clients where code = '{data}'", CommandType.Text);

                dt = conn.Ejecutar_Comando_De_Retorno_Tabular();
            }
        }

        private IDBAdoConnection SetConnection()
        {
            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\DatabaseScripts\dbAccess.mdb;";
            return DBConnFactory.Obtener_Nueva_Conexion("System.Data.OleDb", connstring);
        }
    }
}