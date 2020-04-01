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
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={Controllers.SoftwareInfo.DatabaseName};Integrated Security=True");

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
                string query = $"SELECT COUNT(*) FROM {TableName} WHERE {args}";
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

        public double Sum(string TableName, string Colname)
        {
            double count = 0;
            try
            {
                string query = "SELECT SUM(" + Colname + ") FROM " + TableName;
                SqlCommand cmd = new SqlCommand(query, Conn);
                Conn.Open();
                count = Convert.ToDouble(cmd.ExecuteScalar());
                Conn.Close();
                return count;
            }
            catch
            {
                return count;
            }
        }

        /// <summary>
        /// <b>param</b> <c>TableName</c><br></br>
        /// <b>param</b> <c>Colname</c><br></br>
        /// <b>param</b> <c>args Eg: id = 1 AND name = somename</c><br></br>
        /// <b>return sum</b>
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Colname"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public double SumByArgs(string TableName, string Colname, string args)
        {
            //string[,] args = new string[,];

            double count = 0;
            try
            {
                string query;
                query = $"SELECT SUM({ Colname }) FROM { TableName } WHERE { args } ";

                SqlCommand cmd = new SqlCommand(query, Conn);
                Conn.Open();
                count = Convert.ToDouble(cmd.ExecuteScalar());
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