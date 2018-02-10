using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace JP.Base.DataAccessModel.DBServices.ConnectionManagement
{
    internal class SQLServerDBConnection : DBCommonConnection
    {
        public SQLServerDBConnection()
            : base(ConfigurationSettings.AppSettings["DataProvider"], ConfigurationSettings.AppSettings["ConnectionString"])
        {
        }

        public SQLServerDBConnection(string DataProvider, string ConnString)
            : base(DataProvider, ConnString)
        {
        }

        protected internal override void Agregar_Parametro(string NombreParam, object Valor, ParameterDirection Direccion, DbType Tipo, int size)
        {
            try
            {
                SqlParameter Param = new SqlParameter(NombreParam, Valor);
                Param.Direction = Direccion;
                Param.Size = size;
                Param.DbType = Tipo;

                ((SqlCommand)_DBCmd).Parameters.Add(Param);
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

            DbDataReader Reader = null;

            try
            {
                Reader = _DBCmd.ExecuteReader();
                DTReturn = Reader_To_Table(ref Reader, true);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Reader != null)
                {
                    Reader.Close();
                    Reader.Dispose();
                    Reader = null;
                }
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

        ~SQLServerDBConnection()
        {
            Dispose(false);
        }
    }
}