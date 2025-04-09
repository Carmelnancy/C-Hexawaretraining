using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoanManagement.Util
{
    public class DBUtil
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("data source = DESKTOP-S58REAU\\SQLEXPRESS;database =LoanManagement;trusted_connection = true;");
            con.Open();
            return con;
        }
    }
}
