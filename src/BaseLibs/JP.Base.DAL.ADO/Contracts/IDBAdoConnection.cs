using JP.Base.DAL.ADO.Commands;
using System;
using System.Collections.Generic;
using System.Data;

namespace JP.Base.DAL.ADO.Contracts
{
    public interface IDbAdoConnection : IDisposable
    {
        bool IsDisposed { get; }

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
        void AddCommandParameter(IEnumerable<ParameterData> param);

        /// <summary>
        /// Cierra la conexion a la base de datos
        /// </summary>
        /// <param name="DeshacerTransaccion">
        /// indica si debe deshacerse la transaccion pendiente, en caso contrario, la misma no se altera
        /// </param>
        /// <returns>True si el proceso retorna exitosamente</returns>
        void Close(bool rollbackTransaction = false);

        /// <summary>
        /// Crea un comando y lo establece como el tipo especificado y con el timeout especificado
        /// </summary>
        /// <param name="NombreCMD">Indica el nombre del stored procedure</param>
        /// <param name="type">Indica el tipo del comando</param>
        /// <param name="timeout">Indica el timeout del comando</param>
        void CreateCommand(CommandData data);

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

        /// <summary>
        /// Abre una conexion a la base de datos especificada
        /// </summary>
        /// <param name="beginTransaction">
        /// Indica si la apertura de la conexion implica iniciar una transaccion
        /// </param>
        /// <returns>True si la conexion se abrio exitosamente</returns>
        bool Open(bool beginTransaction = false);
    }
}