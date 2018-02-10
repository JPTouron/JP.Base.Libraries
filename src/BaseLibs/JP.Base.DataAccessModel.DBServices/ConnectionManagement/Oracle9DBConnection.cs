using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace JP.Base.DataAccessModel.DBServices.ConnectionManagement
{
    internal class Oracle9DBConnection : DBCommonConnection
    {
        public Oracle9DBConnection()
            : base(ConfigurationSettings.AppSettings["DataProvider"], ConfigurationSettings.AppSettings["ConnectionString"])
        {
        }

        public Oracle9DBConnection(string DataProvider, string ConnString)
            : base(DataProvider, ConnString)
        {
        }

        protected internal override void Agregar_Parametro(string NombreParam, object Valor, ParameterDirection Direccion, DbType Tipo, int size)
        {
            try
            {
                OracleParameter Param = new OracleParameter(NombreParam, Valor);
                Param.Direction = Direccion;
                Param.Size = size;
                Param.DbType = Tipo;

                ((OracleCommand)_DBCmd).Parameters.Add(Param);
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
                OracleParameter prm = new OracleParameter(ConfigurationManager.AppSettings["NOMBRE_CURSOR_RETORNO_ESCALAR"], OracleType.Number);
                prm.Direction = ParameterDirection.Output;

                _DBCmd.Parameters.Add(prm);
                _DBCmd.ExecuteScalar();

                oReturn = _DBCmd.Parameters[ConfigurationManager.AppSettings["NOMBRE_CURSOR_RETORNO_ESCALAR"]].Value;
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
            DataSet DSReturn = new DataSet();

            try
            {
                OracleParameter prm = new OracleParameter(ConfigurationManager.AppSettings["NOMBRE_CURSOR_RETORNO_TABULAR"], OracleType.Cursor);
                prm.Direction = ParameterDirection.Output;
                _DBCmd.Parameters.Add(prm);

                OracleDataAdapter adapter = new OracleDataAdapter((OracleCommand)_DBCmd);

                adapter.Fill(DSReturn);
                adapter.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return DSReturn.Tables[0];
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

        ~Oracle9DBConnection()
        {
            Dispose(false);
        }
    }
}