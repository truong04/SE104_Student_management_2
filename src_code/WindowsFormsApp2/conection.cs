using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp2
{
    internal class conection
    {
        private static string stringConnection = @"Data Source=LAPTOP-P0OHVEG9;Initial Catalog=QLHSnew;Integrated Security=True;Encrypt=False";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }

    }
}
