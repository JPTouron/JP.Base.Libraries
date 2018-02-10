namespace JP.Base.Common.Errors
{
    /// <summary>
    /// Representa una ubicacion de error con Nro de Linea, Nombre de Metodo, y Archivo del error
    /// </summary>
    public class ExceptionLocationData
    {
        private string fileName = string.Empty;
        private int line = 0;
        private string method = string.Empty;

        /// <param name="Linea">Nro de linea del error</param>
        /// <param name="Metodo">Metodo donde se genero el error</param>
        /// <param name="Archivo">Nombre del archivo donde el error se genero</param>
        public ExceptionLocationData(int errLine, string errMethod, string errFileName)
        {
            line = errLine;
            method = errMethod;
            fileName = errFileName;
        }

        public string FileName
        {
            get { return fileName; }
        }

        public int Line
        {
            get { return line; }
        }

        public string Method
        {
            get { return method; }
        }
    }
}