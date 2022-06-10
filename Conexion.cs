using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Creditos
{
    class Conexion
    {

        public static SqlConnection Conectar()
        {
            SqlConnection conectar = new SqlConnection("SERVER=DESKTOP-0MQU2P1\\MSSQLSERVER01;DATABASE=Creditos; Integrated security=true;");

            conectar.Open();
            return conectar;
        }

    }
}
