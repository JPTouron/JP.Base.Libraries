using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Factories;
using System;
using System.Data;

namespace ADO.DAL.POC
{
    internal class SqlInterface
    {
        public void GetData()
        {
            DataTable dt;

            using (var conn = SetConnection())
            {
                conn.Open();
                conn.CreateCommand("select * from Clients", CommandType.Text);

                dt = conn.ExecuteReaderCommand();
            }
        }

        public void SetData()
        {
            var rnd = new Random();
            var data = rnd.Next().ToString().Substring(0, 7).PadLeft(8, '0');
            using (var conn = SetConnection())
            {
                conn.Open();
                conn.CreateCommand($"INSERT INTO Clients (Code,Name) VALUES('{data}','Name{data}')", CommandType.Text);

                conn.ExecuteNonQueryCommand();
            }

            data = rnd.Next().ToString().Substring(0, 7).PadLeft(8, '0');
            using (var conn = SetConnection())
            {
                conn.Open();
                conn.CreateCommand($"INSERT INTO Clients (Code,Name) VALUES('{data}','Name{data}')", CommandType.Text);

                conn.ExecuteNonQueryCommand();
            }
        }

        private IDbAdoConnection SetConnection()
        {
            var connstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ADO.DAL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return DbConnFactory.Obtener_Nueva_Conexion("System.Data.SqlClient", connstring);
        }
    }
}