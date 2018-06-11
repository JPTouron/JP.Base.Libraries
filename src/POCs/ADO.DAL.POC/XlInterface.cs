using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.Implementations.Factories;
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
                conn.Open();
                conn.CreateCommand(new JP.Base.DAL.ADO.Commands.CommandData { CommandText = "UPDATE [Hoja1$] SET puesto =NULL, Fecha=null, Hora= null, Elaboracion=null,Codigo=null, Bruto=null, Tara=null, Neto=null  WHERE puesto =2" });

                conn.ExecuteNonQueryCommand();
            }
        }

        public void GetData()
        {
            DataTable dt;

            using (var conn = SetConnectionOleDb())
            {
                conn.Open();
                conn.CreateCommand(new JP.Base.DAL.ADO.Commands.CommandData { CommandText = "select * from [Hoja1$]" });

                dt = conn.ExecuteReaderCommand();
            }
        }

        public void SetData()
        {
            using (var conn = SetConnectionOleDb())
            {
                conn.Open();
                conn.CreateCommand(new JP.Base.DAL.ADO.Commands.CommandData { CommandText = $"insert into [Hoja1$] VALUES (1,'{DateTime.Now.Date.ToShortDateString()}','{DateTime.Now.ToShortTimeString()}',13,'009',12,10,9)" });
                //conn.Agregar_Parametro_A_Comando(new ParameterData {  });

                conn.ExecuteNonQueryCommand();
            }
        }

        private IDbAdoConnection SetConnectionOleDb()
        {
            var connstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=XlFile.xls;Mode=ReadWrite;Extended Properties=""Excel 8.0;HDR=Yes;"";Persist Security Info=False";

            return new DbConnFactory().GetConnection("System.Data.OleDb", connstring);
        }
    }
}