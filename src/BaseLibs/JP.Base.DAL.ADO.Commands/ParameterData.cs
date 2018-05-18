using System.Data;

namespace JP.Base.DAL.ADO.Commands
{
    public class ParameterData
    {
        public ParameterData()
        {

        }

        public ParameterData(string Nombre, object Valor, ParameterDirection Direccion, DbType TipoParam, int ParamSize)
        {
            Constructor(Nombre, Valor, Direccion, TipoParam, ParamSize);
        }

        public ParameterData(string Nombre, object Valor, ParameterDirection Direccion, DbType TipoParam)
        {
            Constructor(Nombre, Valor, Direccion, TipoParam, 0);
        }

        public ParameterData(string Nombre, object Valor, DbType TipoParam)
        {
            Constructor(Nombre, Valor, ParameterDirection.Input, TipoParam, 0);
        }

        public ParameterDirection DireccionParametro { get; set; }

        public string NombreParametro { get; set; }

        public int Size { get; set; }

        public DbType Tipo { get; set; }

        public object ValorParametro { get; set; }

        private void Constructor(string Nombre, object Valor, ParameterDirection Direccion, DbType TipoParam, int ParamSize)
        {
            NombreParametro = Nombre;
            ValorParametro = Valor;
            DireccionParametro = Direccion;
            Tipo = TipoParam;
            Size = ParamSize;
        }
    }
}