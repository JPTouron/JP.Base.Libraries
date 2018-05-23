using JP.Base.DAL.ADO.Commands;
using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JP.Base.DAL.ADO.Tests
{
    [TestClass]
    public class DbIntegratedTests
    {
        private IDbConnFactory factory;

        [TestInitialize]
        public void Initialize()
        {
            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\dbAccess.mdb;";

            factory = new DbConnFactory("", connstring);
        }

        [TestMethod]
        public void TestTransactions()
        {
            var count = -1;
            var id = -2;
            var name = "";

            using (var conn = factory.GetConnection())
            {
                conn.Open();
                conn.BeginTransaction();

                conn.CreateCommand(new CommandData { CommandText = "insert into clients (name, code) values ('namey test', 'codey testy')" });
                conn.ExecuteScalarCommand();

                conn.CreateCommand(new CommandData { CommandText = "SELECT @@Identity" });

                id = Convert.ToInt32(conn.ExecuteScalarCommand());
                
                conn.CreateCommand(new CommandData { CommandText = $"update clients set code='meh...' where id ={id}" });
                conn.ExecuteNonQueryCommand();

                conn.CommitTransaction();
                conn.Close();

                conn.Open();

                conn.CreateCommand(new CommandData { CommandText = $"select name from clients where id = {id}" });
                name = conn.ExecuteScalarCommand().ToString();

                conn.BeginTransaction();

                conn.CreateCommand(new CommandData { CommandText = $"update clients set name='thisWasTested' where id ={id}" });
                conn.ExecuteNonQueryCommand();

                conn.RollbackTansacton();

                conn.CreateCommand(new CommandData { CommandText = $"select name from clients where id = {id}" });
                name = conn.ExecuteScalarCommand().ToString();



                conn.CreateCommand(new CommandData { CommandText = $"select * from clients where id = {id}" });
                var dt = conn.ExecuteReaderCommand();

                count = dt.Rows.Count;


            }

            Assert.IsTrue(count == 1);
            Assert.IsTrue(name == "namey test");


        }
    }
}