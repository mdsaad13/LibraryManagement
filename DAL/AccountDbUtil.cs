using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryManagement.DAL
{
    public class AccountDbUtil
    {
        /*
         * Conn = connection string of database
         */
        private readonly SqlConnection Conn = new SqlConnection("Data Source=localhost;Initial Catalog=LibraryManagement;Integrated Security=True");

        public int CreateAccount(Admin adminObj)
        {
            int rows = 0;
            try
            {
                /*
                 * Creating a SQL prepared statement
                 */
                string query = "INSERT INTO admin (name, email, password)" +
                        " VALUES(@name_bind, @email_bind, @password_bind)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                /*
                 * Binding the SQL prepared statement with values
                 */
                cmd.Parameters.Add(new SqlParameter("name_bind", adminObj.Name));
                cmd.Parameters.Add(new SqlParameter("email_bind", adminObj.Email));
                cmd.Parameters.Add(new SqlParameter("password_bind", adminObj.Password));

                /*
                 * Opening sql connection
                 */
                Conn.Open();

                /*
                 * @return rows = number of rows affected
                 */
                rows = cmd.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {
                /*
                 * Closing sql connection
                 */
                Conn.Close();
            }
            return rows;
        }

        public Admin Login(string Email, string Password)
        {
            /*
             * Creating object of modal UserInfo to store return values
             */
            Admin returnobj = new Admin();

            /*
             * Setting variable `ID` to 0
             * 
             * If user exist then `ID` will be that perticular users `ID` 
             */
            returnobj.ID = 0;
            try
            {
                /*
                 * Creating a SQL prepared statement
                 */
                string query = "select * from admin where email = @email_bind and password = @password_bind";
                SqlCommand cmd = new SqlCommand(query, Conn);

                /*
                 * Binding the SQL prepared statement with values
                 */
                cmd.Parameters.Add(new SqlParameter("email_bind", Email));
                cmd.Parameters.Add(new SqlParameter("password_bind", Password));

                /*
                 * Creating object of SqlDataAdapter
                 * Used to retrieve data
                 */
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                /*
                 * Creating object of DataTable
                 * Used to store retrieved data in tabular format
                 */
                DataTable dt = new DataTable();

                /*
                 * Storing the `SqlDataAdapter` data into `DataTable`
                 */
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    returnobj.ID = Convert.ToInt32(dt.Rows[0]["id"]);
                    returnobj.Name = Convert.ToString(dt.Rows[0]["name"]);
                    returnobj.Email = Convert.ToString(dt.Rows[0]["email"]);
                }
            }
            catch
            { }
            return returnobj;
        }

        public Admin GetAdminById(int userID)
        {
            /*
             * Creating object of modal UserInfo to store return values
             */
            Admin returnobj = new Admin();

            try
            {
                /*
                 * Creating a SQL prepared statement
                 */
                string query = "SELECT * FROM admin WHERE id = @userID_bind";
                SqlCommand cmd = new SqlCommand(query, Conn);

                /*
                 * Binding the SQL prepared statement with values
                 */
                cmd.Parameters.Add(new SqlParameter("userID_bind", userID));

                /*
                 * Creating object of SqlDataAdapter
                 * Used to retrieve data
                 */
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                /*
                 * Creating object of DataTable
                 * Used to store retrieved data in tabular format
                 */
                DataTable dt = new DataTable();

                /*
                 * Storing the `SqlDataAdapter` data into `DataTable`
                 */
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    returnobj.ID = Convert.ToInt32(dt.Rows[0]["id"]);
                    returnobj.Name = Convert.ToString(dt.Rows[0]["name"]);
                    returnobj.Email = Convert.ToString(dt.Rows[0]["email"]);
                }
            }
            catch
            {}
            return returnobj;

        }
    }
}