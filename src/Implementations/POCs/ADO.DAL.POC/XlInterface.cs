using JP.Base.DAL.ADO.ConnectionManagement;
using System;
using System.Data;

namespace ADO.DAL.POC
{
    internal class XlInterface

    {
        public void DeleteDataWithOleDb()
        {
            using (var conn = SetConnectionOleDb())
            {
                conn.Abrir_Conexion();
                conn.Crear_Comando("UPDATE [Hoja1$] SET puesto =NULL, Fecha=null, Hora= null, Elaboracion=null,Codigo=null, Bruto=null, Tara=null, Neto=null  WHERE puesto =2", CommandType.Text);

                conn.Ejecutar_Comando_Sin_Retorno();
            }
        }

        public void GetData()
        {
            DataTable dt;

            using (var conn = SetConnectionOleDb())
            {
                conn.Abrir_Conexion();
                conn.Crear_Comando("select * from [Hoja1$]", CommandType.Text);

                dt = conn.Ejecutar_Comando_De_Retorno_Tabular();
            }
        }

        public void SetData()
        {
            using (var conn = SetConnectionOleDb())
            {
                conn.Abrir_Conexion();
                conn.Crear_Comando($"insert into [Hoja1$] VALUES (1,'{DateTime.Now.Date.ToShortDateString()}','{DateTime.Now.ToShortTimeString()}',13,'009',12,10,9)", CommandType.Text);
                //conn.Agregar_Parametro_A_Comando(new ParameterData {  });

                conn.Ejecutar_Comando_Sin_Retorno();
            }
        }

        private IDBAdoConnection SetConnectionOleDb()
        {
            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Captura_4Ado Pesadas Set.xls;Mode=ReadWrite;Extended Properties=""Excel 8.0;HDR=Yes;"";Persist Security Info=False";

            return DBConnFactory.Obtener_Nueva_Conexion("System.Data.OleDb", connstring);
        }
    }
}