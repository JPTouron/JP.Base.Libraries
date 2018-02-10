using System.Data;
using System.Data.Common;

namespace JP.Base.DataAccessModel.DBServices.ConnectionManagement
{
    /// <summary>
    /// Esta clase solo se utiliza para convertir un IDataReader a un DataTable
    /// </summary>
    internal class DataReaderAdapter : DbDataAdapter
    {
        public int Fill_From_Reader(DataTable dataTable, IDataReader dataReader)
        {
            return this.Fill(dataTable, dataReader);
        }
    }
}