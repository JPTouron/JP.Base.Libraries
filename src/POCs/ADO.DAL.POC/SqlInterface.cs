using JP.Base.DAL.ADO.Commands;
using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Factories;
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
                conn.CreateCommand(new CommandData { CommandText = "select * from Clients", CommandType = CommandType.Text });

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
                conn.CreateCommand(new CommandData { CommandText = $"INSERT INTO Clients (Code,Name) VALUES('{data}','Name{data}')", CommandType = CommandType.Text });

                conn.ExecuteNonQueryCommand();
            }

            data = rnd.Next().ToString().Substring(0, 7).PadLeft(8, '0');
            using (var conn = SetConnection())
            {
                conn.Open();
                conn.CreateCommand(new JP.Base.DAL.ADO.Commands.CommandData { CommandText = $"INSERT INTO Clients (Code,Name) VALUES('{data}','Name{data}')" });

                conn.ExecuteNonQueryCommand();
            }
        }

        private IDbAdoConnection SetConnection()
        {
            var connstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ADO.DAL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return new DbConnFactory().GetConnection("System.Data.SqlClient", connstring);
        }
    }
}