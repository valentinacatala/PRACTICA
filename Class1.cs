using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRACTICA
{
    internal class Class1
    {
        string cadena;
        string sql;
        SQLiteDataAdapter adaptador;
        DataTable tabla = new DataTable();
       

        public DataTable personas(string sql)
        {
            cadena = "DataSource=ejemplo.db";
            adaptador = new SQLiteDataAdapter(sql, cadena);
            adaptador.Fill(tabla);
            return tabla;
        }
        
    }
}

