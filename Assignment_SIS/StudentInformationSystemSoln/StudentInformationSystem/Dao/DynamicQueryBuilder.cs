using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Util;


namespace StudentInformationSystem.Dao
{
    public class DynamicQueryBuilder
    {
        static SqlConnection con = null;
        static SqlCommand cmd;
        static SqlDataReader dr;
        public static void MakeDynamicQuery()
        {
            Console.Write("Enter table name: ");
            string table = Console.ReadLine();

            Console.Write("Enter columns to display (comma-separated): ");
            string columns = Console.ReadLine();

            Console.Write("Enter WHERE condition (or press Enter to skip): ");
            string where = Console.ReadLine();

            Console.Write("Enter sorting criteria (or press Enter to skip): ");
            string orderBy = Console.ReadLine();

            StringBuilder sb = new StringBuilder();
                sb.Append($"SELECT {columns} FROM {table}");

            if (!string.IsNullOrWhiteSpace(where))
                sb.Append($" WHERE {where}");

            if (!string.IsNullOrWhiteSpace(orderBy))
                sb.Append($" ORDER BY {orderBy}");
            string s= sb.ToString();

            try
            {
                con = DBConnection.GetConnection();
                cmd = new SqlCommand(s, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        Console.Write($"{dr.GetName(i)}: {dr[i]} \t");
                    }
                    Console.WriteLine();
                }

                dr.Close();
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error executing query: " + ex.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    
       

    }
}
