//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
//namespace Ecommerce.Util
//{
//    public class PropertyUtil
//    {
//        public static string GetConnectionString()
//        {
//            return "data source = DESKTOP-S58REAU\\SQLEXPRESS;database = EcommerceApp;trusted_connection = true;";
//        }
//    }
//}

using System.IO;

namespace Ecommerce.Util
{
    public class PropertyUtil
    {
        public static string GetConnectionString(string fileName = "db.properties")
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Property file not found: " + fileName);

            string[] lines = File.ReadAllLines(fileName);
            string connectionString = string.Join(";", lines);
            return connectionString;
        }
    }
}
