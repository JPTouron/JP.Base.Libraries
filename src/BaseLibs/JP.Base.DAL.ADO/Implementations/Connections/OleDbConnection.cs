using JP.Base.DAL.ADO.Implementations.Connections.Base;
using System.Configuration;
using System.Data;
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

        protected internal override void AddParameter(string NombreParam, object Valor, ParameterDirection Direccion, DbType Tipo, int size)
        {
            OleDbParameter Param = new OleDbParameter(NombreParam, Valor);
            Param.Direction = Direccion;
            Param.Size = size;
            Param.DbType = Tipo;

            ((OleDbCommand)command).Parameters.Add(Param);
        }

        protected internal override int ExecuteNonQuery()
        {
            var result = command.ExecuteNonQuery();

            return result;
        }

        protected internal override DataTable ExecuteReader()
        {
            var reader = command.ExecuteReader();
            var result = new DataTable();

            result.Load(reader);

            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }

            return result;
        }

        protected internal override object ExecuteScalar()
        {
            var result = command.ExecuteScalar();//ejecutar el comando

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            // Release managed resources. Release unmanaged resources. Set large fields to null. Call
            // Dispose on your base class.

            base.Dispose(disposing);
        }

        ~OleDbConnection()
        {
            Dispose(false);
        }
    }
}