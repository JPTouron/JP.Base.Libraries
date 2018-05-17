using JP.Base.DAL.ADO.Implementations.Connections.Base;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace JP.Base.DAL.ADO.Implementations.Connections
{
    internal class OleDbConnection : DbAdoConnection
    {
        public OleDbConnection()
            : base(ConfigurationManager.AppSettings["DataProvider"], ConfigurationManager.AppSettings["ConnectionString"])
        {
        }

        public OleDbConnection(string DataProvider, string ConnString)
            : base(DataProvider, ConnString)
        {
        }

        ~OleDbConnection()
        {
            Dispose(false);
        }

        protected internal override void Agregar_Parametro(string NombreParam, object Valor, ParameterDirection Direccion, DbType Tipo, int size)
        {
            try
            {
                OleDbParameter Param = new OleDbParameter(NombreParam, Valor);
                Param.Direction = Direccion;
                Param.Size = size;
                Param.DbType = Tipo;

                ((OleDbCommand)_DBCmd).Parameters.Add(Param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected internal override object Ejecutar_CMD_Escalar()
        {
            object oReturn = null;

            try
            {
                oReturn = _DBCmd.ExecuteScalar();//ejecutar el comando
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oReturn;
        }

        protected internal override int Ejecutar_CMD_Sin_Retorno()
        {
            int iReturn = 0;

            try
            {
                iReturn = _DBCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        protected internal override DataTable Ejecutar_CMD_Tabular()
        {
            DataTable DTReturn = null;
            try
            {
                DbDataReader Reader;
                Reader = _DBCmd.ExecuteReader();
                DTReturn = Reader_To_Table(ref Reader, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return DTReturn;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            // Release managed resources.
            // Release unmanaged resources.
            // Set large fields to null.
            // Call Dispose on your base class.

            base.Dispose(disposing);
        }
    }
}