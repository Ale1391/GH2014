using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FrbaHotel
{
    class Variables
    {
        public static int hotel_id = 0;
        public static string fecha_sistema = ConfigurationManager.AppSettings["fecha_sistema"].ToString();
        public static string usuario = "";
        public static string tipo_usuario = "";
        public static string connectionStr = ConfigurationManager.ConnectionStrings["gdd_db"].ConnectionString;
    }
}
