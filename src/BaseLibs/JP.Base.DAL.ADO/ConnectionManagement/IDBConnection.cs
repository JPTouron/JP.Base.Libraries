using System;
using System.Collections.Generic;
using System.Data;

namespace JP.Base.DAL.ADO.ConnectionManagement
{
    public interface IDBConnection : IDisposable
    {
        /// <summary>
        /// Abre una conexion a la base de datos especificada
        /// </summary>
        /// <param name="IniciarTransaccion">Indica si la apertura de la conexion implica iniciar una transaccion</param>
        /// <returns>True si la conexion se abrio exitosamente</returns>
        bool Abrir_Conexion(bool IniciarTransaccion);

        /// <summary>
        /// Abre una conexion a la base de datos especificada sin iniciar una transaccion
        /// </summary>
        /// <returns>True si la conexion se abrio exitosamente</returns>
        bool Abrir_Conexion();

        /// <summary>
        /// Invoca al metodo abstracto Agregar_Parametro con los parametros especificados, y establece los valores null del parametro si
        /// el parametro valor es un String.Empty o un DateTime.MinValue o null
        /// </summary>
        /// <param name="ParameterData">Nombre, valor y direccion del parametro</param>
        void Agregar_Parametro_A_Comando(ParameterData Parametro);

        /// <summary>
        /// Invoca al metodo abstracto Agregar_Parametro con los parametros especificados, y establece los valores null del parametro si
        /// el parametro valor es un String.Empty o un DateTime.MinValue o null
        /// </summary>
        /// <param name="Parametros">Lista de datos del parametro como nombre, valor y direccion</param>
        void Agregar_Parametros_A_Comando(ref List<ParameterData> Parametros);

        /// <summary>
        /// Cierra la conexion a la base de datos
        /// </summary>
        /// <param name="DeshacerTransaccion">indica si debe deshacerse la transaccion pendiente, en caso contrario, la misma no se altera</param>
        /// <returns>True si el proceso retorna exitosamente</returns>
        bool Cerrar_Conexion(bool deshacerTransaccion);

        /// <summary>
        /// Cierra la conexion a la base de datos sin alterar la transaccion
        /// </summary>
        /// <returns>True si el proceso finaliza exitosamente</returns>
        bool Cerrar_Conexion();

        /// <summary>
        /// Crea un comando y lo establece como el tipo especificado y con el timeout especificado
        /// </summary>
        /// <param name="NombreCMD">Indica el nombre del stored procedure</param>
        /// <param name="TipoComando">Indica el tipo del comando</param>
        /// <param name="TimeOut">Indica el timeout del comando</param>
        void Crear_Comando(string NombreCMD, CommandType TipoComando, int TimeOut);

        /// <summary>
        /// Crea un comando y lo establece como el tipo especificado el timeout es 0
        /// </summary>
        /// <param name="NombreCMD">Indica el nombre del stored procedure</param>
        /// <param name="TipoComando">Indica el tipo del comando</param>
        void Crear_Comando(string NombreCMD, CommandType TipoComando);

        /// <summary>
        /// Crea un comando y lo establece como SP, el timeout del comando se establece en 0 y el tipo de comando es Stored Procedure
        /// </summary>
        /// <param name="NombreCMD">Indica el nombre del stored procedure</param>
        void Crear_Comando(string NombreCMD);

        /// <summary>
        /// Ejecuta un comando SQL retornando el escalar correspondiente
        /// </summary>
        /// <returns>Retorna el valor escalar obtenido</returns>
        object Ejecutar_Comando_De_Retorno_Escalar();

        /// <summary>
        /// Ejecuta un comando SQL retornando el DataTable correspondiente
        /// </summary>
        /// <returns>Retorna el DataTable obtenido</returns>
        DataTable Ejecutar_Comando_De_Retorno_Tabular();

        /// <summary>
        /// Ejecuta un comando SQL retornando la cantidad de filas afectadas
        /// </summary>
        /// <returns>Retorna el nro de filas afectadas</returns>
        int Ejecutar_Comando_Sin_Retorno();
    }
}