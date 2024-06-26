using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp2
{
    internal class modify
    {
        public modify()
        {
        }
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public List<Taikhoan> Taikhoans(string query) //check tai khoan
        {
            List<Taikhoan> Taikhoans = new List<Taikhoan>();
            using (SqlConnection sqlConnection = conection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while(dataReader.Read())
                {
                    Taikhoans.Add(new Taikhoan(dataReader.GetString(0), dataReader.GetString(1)));
                }
                sqlConnection.Close();
            }

            return Taikhoans;
        }
        public void Command(string query) // dung de dang ki tai khoan
        {
            using(SqlConnection sqlConnection = conection.GetSqlConnection()) 
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query,sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

            }
        }
    }
}
