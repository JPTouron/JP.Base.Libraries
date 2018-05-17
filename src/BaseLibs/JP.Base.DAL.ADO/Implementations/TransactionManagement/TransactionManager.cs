using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace JP.Base.DAL.ADO.Implementations.TransactionManagement
{
    internal class TransactionManager : ITransactionManager, IDisposable
    {
        private static string _LastTransactionId = "0";
        private string _ConnString;
        private string _DataProvider;
        private List<TransactionData> _TransactionConnections = new List<TransactionData>();

        public TransactionManager(string DataProvider, string ConnString)
        {
            _DataProvider = DataProvider;
            _ConnString = ConnString;
        }

        public TransactionManager()
        {
            _DataProvider = ConfigurationManager.AppSettings["DataProvider"];
            _ConnString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Finalizar_Transaccion(string IDTransaccion, bool pDeshacerTransaccion)
        {
            IDbAdoConnection DBConn = Obtener_Conexion_De_Transaccion(IDTransaccion);
            DBConn.Close(pDeshacerTransaccion);

            foreach (TransactionData TranData in _TransactionConnections)
            {
                if (TranData.TransactionID == IDTransaccion)
                {
                    _TransactionConnections.Remove(TranData);
                    break;
                }
            }
        }

        public void Finalizar_Transaccion(string IDTransaccion)
        {
            Finalizar_Transaccion(IDTransaccion, false);
        }

        public string Iniciar_Transaccion(out IDbAdoConnection DBConnTransaccionada)
        {
            DBConnTransaccionada = Obtener_Nueva_Conexion();
            DBConnTransaccionada.Open(true);

            TransactionData TranData = new TransactionData();

            TranData.TransactionID = Crear_Id_Transaccion();
            TranData.DBConn = DBConnTransaccionada;

            _TransactionConnections.Add(TranData);

            return TranData.TransactionID;
        }

        public IDbAdoConnection Obtener_Conexion_De_Transaccion()
        {
            return Obtener_Conexion_De_Transaccion(null);
        }

        public IDbAdoConnection Obtener_Conexion_De_Transaccion(string pIdTransaccion)
        {
            IDbAdoConnection DBConn = null;
            bool bConnEncontrada = false;

            if (!string.IsNullOrEmpty(pIdTransaccion))
            {
                foreach (TransactionData TranData in _TransactionConnections)
                {
                    if (String.Equals(TranData.TransactionID, pIdTransaccion, StringComparison.InvariantCultureIgnoreCase))
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
            {
                //managed
                _LastTransactionId = "0";
            }

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

        private string Crear_Id_Transaccion()
        {
            _LastTransactionId = (Convert.ToInt32(_LastTransactionId) + 1).ToString();
            return _LastTransactionId;
        }

        private IDbAdoConnection Obtener_Nueva_Conexion()
        {
            return DbConnFactory.Obtener_Nueva_Conexion(_DataProvider, _ConnString);
        }

        private class TransactionData
        {
            public IDbAdoConnection DBConn { get; set; }
            public string TransactionID { get; set; }

            public override bool Equals(object obj)
            {
                return ((TransactionData)obj).TransactionID == this.TransactionID;
            }

            public override int GetHashCode()
            {
                return TransactionID.GetHashCode() + DBConn.GetHashCode();
            }
        }

        ~TransactionManager()
        {
            Dispose(false);
        }
    }
}