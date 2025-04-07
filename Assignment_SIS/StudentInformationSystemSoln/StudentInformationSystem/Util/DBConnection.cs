using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentInformationSystem.Util
{
    public class DBConnection
    {
        public static SqlConnection GetConnection()
        {
           SqlConnection con= new SqlConnection("data source = DESKTOP-S58REAU\\SQLEXPRESS;database = SISDB;trusted_connection = true;");
            con.Open();
            return con;
        }
    }
}
