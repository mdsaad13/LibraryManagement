using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryManagement.DAL
{
    public class GeneralUtil
    {
        /*
         * Conn = connection string of database
         */
        private readonly SqlConnection Conn = new SqlConnection("Data Source=localhost;Initial Catalog=LibraryManagement;Integrated Security=True");

        public int Count(string TableName)
        {
            int count = 0;
            try
            {
                string query = "SELECT COUNT(*) FROM " + TableName;
                SqlCommand cmd = new SqlCommand(query, Conn);
                Conn.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                Conn.Close();
                return count;
            }
            catch
            {
                return count;
            }
        }
        
        public int CountByArgs(string TableName, string args)
        {
            int count = 0;
            try
            {
                string query = "SELECT COUNT(*) FROM " + TableName + " " + args;
                SqlCommand cmd = new SqlCommand(query, Conn);
                Conn.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                Conn.Close();
                return count;
            }
            catch
            {
                return count;
            }
        }
    }
}