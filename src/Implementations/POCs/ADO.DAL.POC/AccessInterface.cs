using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Factories;
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
                conn.CreateCommand(new JP.Base.DAL.ADO.Commands.CommandData { CommandText= "select * from Clients"});

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
                conn.CreateCommand(new JP.Base.DAL.ADO.Commands.CommandData { CommandText = $"INSERT INTO Clients (Code,Name) VALUES('{data}','Name{data}')" });

                conn.ExecuteNonQueryCommand();

                conn.CreateCommand(new JP.Base.DAL.ADO.Commands.CommandData { CommandText = $"select * from Clients where code = '{data}'" });

                dt = conn.ExecuteReaderCommand();
            }
        }

        private IDbAdoConnection SetConnection()
        {
            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\DatabaseScripts\dbAccess.mdb;";
            return new DbConnFactory().GetConnection("System.Data.OleDb", connstring);
        }
    }
}