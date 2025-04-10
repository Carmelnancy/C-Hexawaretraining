using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Util
{
    public class DBConnection
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(PropertyUtil.GetConnectionString());
        }
    }
}



