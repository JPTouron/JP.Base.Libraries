using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Factories;
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
                conn.Open();
                conn.CreateCommand("select * from Clients", CommandType.Text);

                dt = conn.ExecuteReaderCommand();
            }
        }

        public void SetData()
        {
            var rnd = new Random();
            var data = rnd.Next().ToString().Substring(0, 7).PadLeft(8, '0');
            DataTable dt;

            using (var conn = SetConnection())
            {
                conn.Open();
                conn.CreateCommand($"INSERT INTO Clients (Code,Name) VALUES('{data}','Name{data}')", CommandType.Text);

                conn.ExecuteNonQueryCommand();

                conn.CreateCommand($"select * from Clients where code = '{data}'", CommandType.Text);

                dt = conn.ExecuteReaderCommand();
            }
        }

        private IDbAdoConnection SetConnection()
        {
            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\DatabaseScripts\dbAccess.mdb;";
            return DbConnFactory.GetConnection("System.Data.OleDb", connstring);
        }
    }
}