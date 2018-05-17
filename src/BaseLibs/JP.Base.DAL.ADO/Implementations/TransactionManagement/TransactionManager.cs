using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace JP.Base.DAL.ADO.Implementations.TransactionManagement
{
    internal class TransactionManager : ITransactionManager, IDisposable
    {
        private static int lastTransactionId;
        private string _ConnString;
        private string _DataProvider;
        private List<TransactionData> _TransactionConnections = new List<TransactionData>();

        public TransactionManager(string DataProvider, string ConnString)
        {
            _DataProvider = DataProvider;
            _ConnString = ConnString;
            lastTransactionId = 0;
        }

        public TransactionManager() : this(ConfigurationManager.AppSettings["DataProvider"], ConfigurationManager.AppSettings["ConnectionString"])
        {
        }

        ~TransactionManager()
        {
            Dispose(false);
        }

        public int BeginTransaction(out IDbAdoConnection DBConnTransaccionada)
        {
            DBConnTransaccionada = Obtener_Nueva_Conexion();
            DBConnTransaccionada.Open(true);

            TransactionData TranData = new TransactionData();

            TranData.TranId = lastTransactionId++;
            TranData.DBConn = DBConnTransaccionada;

            _TransactionConnections.Add(TranData);

            return TranData.TranId;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void FinishTransaction(int transactionId, bool rollbackTransaction = false)
        {
            IDbAdoConnection DBConn = GetConnection(transactionId);
            DBConn.Close(rollbackTransaction);

            foreach (TransactionData TranData in _TransactionConnections)
            {
                if (TranData.TranId == transactionId)
                {
                    _TransactionConnections.Remove(TranData);
                    break;
                }
            }
        }

        public IDbAdoConnection GetConnection(int transactionId = 0)
        {
            IDbAdoConnection DBConn = null;
            bool bConnEncontrada = false;

            if (transactionId == 0)
            {
                foreach (TransactionData TranData in _TransactionConnections)
                {
                    if (TranData.TranId == transactionId)
                    {
                        DBConn = TranData.DBConn;
                        bConnEncontrada = true;
                        break;
                    }
                }

                if (!bConnEncontrada)
                {
                    throw new Exception("El ID de Transaccion no es válido");
                }
            }
            else
            {
                DBConn = Obtener_Nueva_Conexion();
                DBConn.Open();
            }

            return DBConn;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                lastTransactionId = 0;

            //unmanaged
            if (_TransactionConnections != null)
            {
                foreach (TransactionData TranData in _TransactionConnections)
                {
                    TranData.DBConn.Close(true);
                    _TransactionConnections.Remove(TranData);
                }
                _TransactionConnections = null;
            }
        }

        private IDbAdoConnection Obtener_Nueva_Conexion()
        {
            return DbConnFactory.Obtener_Nueva_Conexion(_DataProvider, _ConnString);
        }

        private class TransactionData
        {
            public IDbAdoConnection DBConn { get; set; }
            public int TranId { get; set; }

            public override bool Equals(object obj)
            {
                return ((TransactionData)obj).TranId == TranId;
            }

            public override int GetHashCode()
            {
                return TranId.GetHashCode() + DBConn.GetHashCode();
            }
        }
    }
}