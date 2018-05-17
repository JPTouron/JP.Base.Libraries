﻿using JP.Base.DAL.ADO.ConnectionManagement;
using System;
using System.Data;

namespace ADO.DAL.POC
{
    internal class SqlInterface
    {
        public void GetData()
        {
            DataTable dt;

            using (var conn = SetConnection())
            {
                conn.Abrir_Conexion();
                conn.Crear_Comando("select * from Clients", CommandType.Text);

                dt = conn.Ejecutar_Comando_De_Retorno_Tabular();
            }
        }

        public void SetData()
        {

            var rnd = new Random();
            var data = rnd.Next().ToString().Substring(0,7).PadLeft(8,'0');
            using (var conn = SetConnection())
            {
                conn.Abrir_Conexion();
                conn.Crear_Comando($"INSERT INTO Clients (Code,Name) VALUES('{data}','Name{data}')", CommandType.Text);

                conn.Ejecutar_Comando_Sin_Retorno();
            }


            data = rnd.Next().ToString().Substring(0, 7).PadLeft(8, '0');
            using (var conn = SetConnection())
            {
                conn.Abrir_Conexion();
                conn.Crear_Comando($"INSERT INTO Clients (Code,Name) VALUES('{data}','Name{data}')", CommandType.Text);

                conn.Ejecutar_Comando_Sin_Retorno();
            }



        }


        private IDBAdoConnection SetConnection()
        {
            var connstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ADO.DAL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return DBConnFactory.Obtener_Nueva_Conexion("System.Data.SqlClient", connstring);
        }
    }
}