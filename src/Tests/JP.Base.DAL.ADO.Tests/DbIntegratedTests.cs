﻿using JP.Base.DAL.ADO.Commands;
using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JP.Base.DAL.ADO.Tests
{
    [TestClass]
    public class DbIntegratedTests
    {
        private IDbConnFactory factory;

        [TestMethod]
        public void Connection_CanGetMoreThanOneDataTableOnOneOpenedConnection()
        {
            using (var conn = factory.GetConnection())
            {
                conn.Open();
                conn.CreateCommand("select * from users");
                var dt = conn.ExecuteReaderCommand();

                conn.CreateCommand("select * from clients");

                var dt1 = conn.ExecuteReaderCommand();

                Assert.IsNotNull(dt);
                Assert.IsNotNull(dt1);
            }
        }

        [TestMethod]
        public void Connection_GetsCreatedOnceAndCanBeClosedAndOpenedThroughout()
        {
            using (var conn = factory.GetConnection())
            {
                var hash = conn.ConnHash;
                Assert.IsTrue(hash == "");

                conn.Open();
                hash = conn.ConnHash;

                Assert.IsTrue(hash != "");

                conn.Close();
                Assert.IsTrue(hash == conn.ConnHash);

                conn.Open();
                Assert.IsTrue(hash == conn.ConnHash);
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\..\..\resources\dbAccess.mdb;";

            factory = new DbConnFactory("system.data.oledb", connstring);
        }

        [TestMethod]
        public void Transactions_PerformInsertAndUpdateWithinCommitedTransactionAndRollbackOnSecondUpdate()
        {
            var count = -1;
            var id = -2;
            var name = "";

            using (var conn = factory.GetConnection())
            {
                conn.Open();
                conn.BeginTransaction();

                conn.CreateCommand(new CommandData { CommandText = "insert into clients (name, code) values ('namey test', 'codey testy')" });
                conn.ExecuteScalarCommand<object>();

                conn.CreateCommand(new CommandData { CommandText = "SELECT @@Identity" });

                id = conn.ExecuteScalarCommand<int>();

                conn.CreateCommand(new CommandData { CommandText = $"update clients set code='meh...' where id ={id}" });
                conn.ExecuteNonQueryCommand();

                conn.CommitTransaction();
                conn.Close();

                conn.Open();

                conn.CreateCommand(new CommandData { CommandText = $"select name from clients where id = {id}" });
                name = conn.ExecuteScalarCommand<string>();

                conn.BeginTransaction();

                conn.CreateCommand(new CommandData { CommandText = $"update clients set name='thisWasTested' where id ={id}" });
                conn.ExecuteNonQueryCommand();

                conn.RollbackTansacton();

                conn.CreateCommand(new CommandData { CommandText = $"select name from clients where id = {id}" });
                name = conn.ExecuteScalarCommand<string>();

                conn.CreateCommand(new CommandData { CommandText = $"select * from clients where id = {id}" });
                var dt = conn.ExecuteReaderCommand();

                count = dt.Rows.Count;
            }

            Assert.IsTrue(count == 1);
            Assert.IsTrue(name == "namey test");
        }
    }
}