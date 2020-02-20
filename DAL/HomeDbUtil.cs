using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryManagement.DAL
{
    public class HomeDbUtil
    {
        /*
         * Conn = connection string of database
         */
        private readonly SqlConnection Conn = new SqlConnection("Data Source=localhost;Initial Catalog=LibraryManagement;Integrated Security=True");

        internal List<Publication> GetAllPublications()
        {
            DataTable td = new DataTable();
            List<Publication> list = new List<Publication>();
            try
            {
                string sqlquery = "SELECT * FROM publication ORDER BY name ASC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Publication obj = new Publication();
                    obj.ID = Convert.ToInt32(row["id"]);

                    GeneralUtil generalCmds = new GeneralUtil();
                    obj.BookCount = generalCmds.CountByArgs("books", "WHERE pubid = " + Convert.ToInt32(row["id"]));

                    obj.Name = Convert.ToString(row["name"]);
                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }

        internal int insertPublication(Publication pubObj)
        {
            int rows = 0;
            try
            {
                /*
                 * Creating a SQL prepared statement
                 */
                string query = "INSERT INTO publication (name) VALUES (@name)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                /*
                 * Binding the SQL prepared statement with values
                 */
                cmd.Parameters.Add(new SqlParameter("name", pubObj.Name));

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

        internal Publication GetPublicationByID(int id)
        {
            DataTable td = new DataTable();
            Publication pubObj = new Publication();
            try
            {
                string sqlquery = "SELECT * FROM publication WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("id", id));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);

                pubObj.ID = Convert.ToInt32(td.Rows[0]["id"]);
                pubObj.Name = Convert.ToString(td.Rows[0]["name"]);

            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return pubObj;
        }

        internal int updatePublication(Publication model)
        {
            int rows = 0;
            try
            {
                string query = "UPDATE publication SET name= @name where id = @id";
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("@name", model.Name));
                cmd.Parameters.Add(new SqlParameter("@id", model.ID));
                Conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return rows;
        }
        
        internal List<Category> GetAllCategory()
        {
            DataTable td = new DataTable();
            List<Category> list = new List<Category>();
            try
            {
                string sqlquery = "SELECT * FROM category ORDER BY name ASC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Category obj = new Category();
                    obj.ID = Convert.ToInt32(row["id"]);

                    GeneralUtil generalCmds = new GeneralUtil();
                    obj.BookCount = generalCmds.CountByArgs("books", "WHERE catid = " + Convert.ToInt32(row["id"]));

                    obj.Name = Convert.ToString(row["name"]);
                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }

        internal int insertCategory(Category Obj)
        {
            int rows = 0;
            try
            {
                /*
                 * Creating a SQL prepared statement
                 */
                string query = "INSERT INTO category (name) VALUES (@name)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                /*
                 * Binding the SQL prepared statement with values
                 */
                cmd.Parameters.Add(new SqlParameter("name", Obj.Name));

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

        internal Category GetCategoryByID(int id)
        {
            DataTable td = new DataTable();
            Category Obj = new Category();
            try
            {
                string sqlquery = "SELECT * FROM category WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("id", id));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);

                Obj.ID = Convert.ToInt32(td.Rows[0]["id"]);
                Obj.Name = Convert.ToString(td.Rows[0]["name"]);

            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return Obj;
        }

        internal int updateCategory(Category model)
        {
            int rows = 0;
            try
            {
                string query = "UPDATE category SET name= @name where id = @id";
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("@name", model.Name));
                cmd.Parameters.Add(new SqlParameter("@id", model.ID));
                Conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return rows;
        }

        internal List<Book> GetAllBooks()
        {
            DataTable td = new DataTable();
            List<Book> list = new List<Book>();
            try
            {
                string sqlquery = "SELECT * FROM books ORDER BY title ASC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Book obj = new Book();
                    obj.ID = Convert.ToInt32(row["id"]);
                    obj.ISBN = Convert.ToInt32(row["isbn"]);
                    obj.Title = Convert.ToString(row["title"]);
                    obj.ImgUrl = Convert.ToString(row["img"]);

                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }

        internal int insertBook(Book Obj)
        {
            int rows = 0;
            try
            {
                /*
                 * Creating a SQL prepared statement
                 */
                string query = "INSERT INTO books (isbn, title, pubid, catid, img, author, edition) VALUES (@isbn, @title, @pubid, @catid, @img, @author, @edition)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                /*
                 * Binding the SQL prepared statement with values
                 */
                cmd.Parameters.Add(new SqlParameter("isbn", Obj.ISBN));
                cmd.Parameters.Add(new SqlParameter("title", Obj.Title));
                cmd.Parameters.Add(new SqlParameter("pubid", Obj.PubID));
                cmd.Parameters.Add(new SqlParameter("catid", Obj.CatID));
                cmd.Parameters.Add(new SqlParameter("img", Obj.ImgUrl));
                cmd.Parameters.Add(new SqlParameter("author", Obj.Author));
                cmd.Parameters.Add(new SqlParameter("edition", Obj.Edition));

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

        internal Book GetBookByID(int id)
        {
            DataTable td = new DataTable();
            Book Obj = new Book();
            try
            {
                string sqlquery = "SELECT * FROM books WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("id", id));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);

                Obj.ID = Convert.ToInt32(td.Rows[0]["id"]);
                Obj.ISBN = Convert.ToInt32(td.Rows[0]["isbn"]);
                Obj.Title = Convert.ToString(td.Rows[0]["title"]);
                Obj.PubID = Convert.ToInt32(td.Rows[0]["pubid"]);
                Obj.CatID = Convert.ToInt32(td.Rows[0]["catid"]);
                Obj.ImgUrl = Convert.ToString(td.Rows[0]["img"]);
                Obj.Author = Convert.ToString(td.Rows[0]["author"]);
                Obj.Edition = Convert.ToString(td.Rows[0]["edition"]);

            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return Obj;
        }
        
        internal Book GetBookByISBN(int isbn)
        {
            DataTable td = new DataTable();
            Book Obj = new Book();
            try
            {
                string sqlquery = "SELECT * FROM books WHERE isbn = @isbn";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("isbn", isbn));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);

                Obj.ID = Convert.ToInt32(td.Rows[0]["id"]);
                Obj.ISBN = Convert.ToInt32(td.Rows[0]["isbn"]);
                Obj.Title = Convert.ToString(td.Rows[0]["title"]);
                Obj.PubID = Convert.ToInt32(td.Rows[0]["pubid"]);
                Obj.CatID = Convert.ToInt32(td.Rows[0]["catid"]);
                Obj.ImgUrl = Convert.ToString(td.Rows[0]["img"]);
                Obj.Author = Convert.ToString(td.Rows[0]["author"]);
                Obj.Edition = Convert.ToString(td.Rows[0]["edition"]);

            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return Obj;
        }

        internal List<Student> GetAllStudents()
        {
            DataTable td = new DataTable();
            List<Student> list = new List<Student>();
            try
            {
                string sqlquery = "SELECT * FROM student ORDER BY id DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Student obj = new Student();
                    obj.ID = Convert.ToInt32(row["id"]);
                    obj.Name = Convert.ToString(row["name"]);
                    obj.Mobile = Convert.ToInt32(row["mobile"]);
                    obj.Email = Convert.ToString(row["email"]);
                    obj.Address = Convert.ToString(row["address"]);
                    obj.City = Convert.ToString(row["city"]);
                    obj.State = Convert.ToString(row["state"]);
                    obj.PinCode = Convert.ToInt32(row["pincode"]);
                    obj.BookIssuedCount = Convert.ToInt32(row["bookIssueCount"]);

                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }

    }
}