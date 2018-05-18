using JP.Base.DAL.ADO.Commands;
using System.Collections.Generic;
using System.Data;

namespace JP.Base.DAL.ADO.UnitOfWork.Contracts
{
    public interface IExecutionData
    {
        /// <summary>
        /// Invoca al metodo abstracto Agregar_Parametro con los parametros especificados, y
        /// establece los valores null del parametro si el parametro valor es un String.Empty o un
        /// DateTime.MinValue o null
        /// </summary>
        /// <param name="ParameterData">Nombre, valor y direccion del parametro</param>
        void AddCommandParameter(ParameterData paramData);

        /// <summary>
        /// Invoca al metodo abstracto Agregar_Parametro con los parametros especificados, y
        /// establece los valores null del parametro si el parametro valor es un String.Empty o un
        /// DateTime.MinValue o null
        /// </summary>
        /// <param name="param">Lista de datos del parametro como nombre, valor y direccion</param>
        void AddCommandParameter(List<ParameterData> param);

        /// <summary>
        /// Crea un comando y lo establece como el tipo especificado y con el timeout especificado
        /// </summary>
        /// <param name="NombreCMD">Indica el nombre del stored procedure</param>
        /// <param name="type">Indica el tipo del comando</param>
        /// <param name="timeout">Indica el timeout del comando</param>
        void CreateCommand(string command, CommandType type = CommandType.Text, int timeout = 1000, List<ParameterData> param = null);

        /// <summary>
        /// Ejecuta un comando SQL retornando la cantidad de filas afectadas
        /// </summary>
        /// <returns>Retorna el nro de filas afectadas</returns>
        int ExecuteNonQueryCommand();

        /// <summary>
        /// Ejecuta un comando SQL retornando el DataTable correspondiente
        /// </summary>
        /// <returns>Retorna el DataTable obtenido</returns>
        DataTable ExecuteReaderCommand();

        /// <summary>
        /// Ejecuta un comando SQL retornando el escalar correspondiente
        /// </summary>
        /// <returns>Retorna el valor escalar obtenido</returns>
        object ExecuteScalarCommand();
    }
}