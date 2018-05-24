using JP.Base.DAL.ADO.Implementations.Connections.Base;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;

namespace JP.Base.DAL.ADO.Implementations.Connections
{
    internal class OracleDbConnection : DbAdoConnection
    {
        public OracleDbConnection()
            : base(ConfigurationManager.AppSettings["DataProvider"], ConfigurationManager.AppSettings["ConnectionString"])
        {
        }

        public OracleDbConnection(string DataProvider, string ConnString)
            : base(DataProvider, ConnString)
        {
        }

        protected internal override void AddParameter(string NombreParam, object Valor, ParameterDirection Direccion, DbType Tipo, int size)
        {
            OracleParameter Param = new OracleParameter(NombreParam, Valor);
            Param.Direction = Direccion;
            Param.Size = size;
            Param.DbType = Tipo;

            ((OracleCommand)command).Parameters.Add(Param);
        }

        protected internal override int ExecuteNonQuery()
        {
            var result = command.ExecuteNonQuery();

            return result;
        }

        protected internal override DataTable ExecuteReader()
        {
            DataSet DSReturn = new DataSet();

            OracleParameter prm = new OracleParameter(ConfigurationManager.AppSettings["NOMBRE_CURSOR_RETORNO_TABULAR"], OracleDbType.RefCursor);
            prm.Direction = ParameterDirection.Output;
            command.Parameters.Add(prm);

            OracleDataAdapter adapter = new OracleDataAdapter((OracleCommand)command);

            adapter.Fill(DSReturn);
            adapter.Dispose();

            return DSReturn.Tables[0];
        }

        protected internal override object ExecuteScalar()
        {
            OracleParameter prm = new OracleParameter(ConfigurationManager.AppSettings["NOMBRE_CURSOR_RETORNO_ESCALAR"], OracleDbType.Int32);
            prm.Direction = ParameterDirection.Output;

            command.Parameters.Add(prm);
            command.ExecuteScalar();

            var result = command.Parameters[ConfigurationManager.AppSettings["NOMBRE_CURSOR_RETORNO_ESCALAR"]].Value;

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            // Release managed resources. Release unmanaged resources. Set large fields to null. Call
            // Dispose on your base class.

            base.Dispose(disposing);
        }

        ~OracleDbConnection()
        {
            Dispose(false);
        }
    }
}