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
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={Controllers.SoftwareInfo.DatabaseName};Integrated Security=True");

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

        internal List<Book> GetAllBooks(int AvailableForIssue = 0)
        {
            DataTable td = new DataTable();
            List<Book> list = new List<Book>();
            try
            {
                string sqlquery;
                if (AvailableForIssue != 0)
                {
                    sqlquery = "SELECT * FROM books WHERE isIssued = 0 ORDER BY title ASC";
                }
                else
                {
                    sqlquery = "SELECT * FROM books ORDER BY title ASC";
                }

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
                    obj.IsIssued = Convert.ToInt32(row["IsIssued"]);

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
                string query = "INSERT INTO books (isbn, title, pubid, catid, img, author, edition) VALUES (@isbn, @title, @pubid, @catid, @img, @author, @edition, @IsIssued)";
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
                cmd.Parameters.Add(new SqlParameter("IsIssued", Obj.IsIssued));

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
                Obj.IsIssued = Convert.ToInt32(td.Rows[0]["IsIssued"]);

            }
            catch (Exception ex)
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
                Obj.IsIssued = Convert.ToInt32(td.Rows[0]["IsIssued"]);

            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return Obj;
        }

        internal List<Student> GetAllStudents(int AvailableForIssue = 0)
        {
            DataTable td = new DataTable();
            List<Student> list = new List<Student>();
            try
            {
                string sqlquery;
                if (AvailableForIssue != 0)
                {
                    sqlquery = "SELECT * FROM student WHERE bookIssueCount < 3 ORDER BY id DESC";
                }
                else
                {
                    sqlquery = "SELECT * FROM student ORDER BY id DESC";
                }

                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Student obj = new Student();
                    obj.ID = Convert.ToInt32(row["id"]);
                    obj.Name = Convert.ToString(row["name"]);
                    obj.Mobile = Convert.ToInt64(row["mobile"]);
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

        internal int insertStudent(Student Obj)
        {
            int rows = 0;
            try
            {
                /*
                 * Creating a SQL prepared statement
                 */
                string query = "INSERT INTO student (name, mobile, email, address, city, state, pincode, bookIssueCount) VALUES (@name, @mobile, @email, @address, @city, @state, @pincode, @bookIssueCount)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                /*
                 * Binding the SQL prepared statement with values
                 */
                cmd.Parameters.Add(new SqlParameter("name", Obj.Name));
                cmd.Parameters.Add(new SqlParameter("mobile", Obj.Mobile));
                cmd.Parameters.Add(new SqlParameter("email", Obj.Email));
                cmd.Parameters.Add(new SqlParameter("address", Obj.Address));
                cmd.Parameters.Add(new SqlParameter("city", Obj.City));
                cmd.Parameters.Add(new SqlParameter("state", Obj.State));
                cmd.Parameters.Add(new SqlParameter("pincode", Obj.PinCode));
                cmd.Parameters.Add(new SqlParameter("bookIssueCount", Obj.BookIssuedCount));

                /*
                 * Opening sql connection
                 */
                Conn.Open();

                /*
                 * @return rows = number of rows affected
                 */
                rows = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
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

        internal Student GetStudentByID(int id, int AvailableForIssue = 0)
        {
            DataTable td = new DataTable();
            Student obj = new Student();
            try
            {
                string sqlquery = "SELECT * FROM student WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("id", id));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);

                obj.ID = Convert.ToInt32(td.Rows[0]["id"]);
                obj.Name = Convert.ToString(td.Rows[0]["name"]);
                obj.Mobile = Convert.ToInt64(td.Rows[0]["mobile"]);
                obj.Email = Convert.ToString(td.Rows[0]["email"]);
                obj.Address = Convert.ToString(td.Rows[0]["address"]);
                obj.City = Convert.ToString(td.Rows[0]["city"]);
                obj.State = Convert.ToString(td.Rows[0]["state"]);
                obj.PinCode = Convert.ToInt32(td.Rows[0]["pincode"]);
                obj.BookIssuedCount = Convert.ToInt32(td.Rows[0]["bookIssueCount"]);
                
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return obj;
        }

        internal int updateStudent(Student Obj)
        {
            int rows = 0;
            try
            {
                string query = "UPDATE student SET name = @name, mobile = @mobile, email = @email, address = @address, city = @city, state = @state, pincode = @pincode where id = @id";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("id", Obj.ID));

                cmd.Parameters.Add(new SqlParameter("name", Obj.Name));
                cmd.Parameters.Add(new SqlParameter("mobile", Obj.Mobile));
                cmd.Parameters.Add(new SqlParameter("email", Obj.Email));
                cmd.Parameters.Add(new SqlParameter("address", Obj.Address));
                cmd.Parameters.Add(new SqlParameter("city", Obj.City));
                cmd.Parameters.Add(new SqlParameter("state", Obj.State));
                cmd.Parameters.Add(new SqlParameter("pincode", Obj.PinCode));
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

        internal List<Issue> GetAllIssue(int status)
        {
            DataTable td = new DataTable();
            List<Issue> list = new List<Issue>();
            try
            {
                string sqlquery = "SELECT * FROM issue WHERE status = @status ORDER BY issueid DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("status", status));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                Conn.Close();
                foreach (DataRow row in td.Rows)
                {
                    Issue obj = new Issue();
                    obj.ID = Convert.ToInt32(row["issueid"]);
                    obj.BookID = Convert.ToInt32(row["bookid"]);
                    obj.StudentID = Convert.ToInt32(row["studentid"]);
                    obj.IssueDate = Convert.ToDateTime(row["datetime"]);
                    obj.ReturnDate = Convert.ToDateTime(row["returndate"]);
                    obj.Status = Convert.ToInt32(row["status"]);
                    obj.ReturnedOn = Convert.ToDateTime(row["returnedon"]);
                    obj.PenaltyAmount = Convert.ToDouble(row["amountCollected"]);

                    Book bookDetails = GetBookByID(obj.BookID);
                    obj.BookISBN = bookDetails.ISBN;
                    obj.BookTitle = bookDetails.Title;

                    Student studentDetails = GetStudentByID(obj.StudentID);
                    obj.StudentName = studentDetails.Name;

                    list.Add(obj);   
                }
            }
            catch (Exception)
            { }
            return list;
        }

        internal int insertIssue(Issue Obj)
        {
            int rows = 0;
            try
            {
                /*
                 * Creating a SQL prepared statement
                 */
                string query = "INSERT INTO issue (bookid, studentid, datetime, returndate, status, returnedon, amountCollected) VALUES (@bookid, @studentid, @datetime, @returndate, @status, @returnedon, @amountCollected)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                /*
                 * Binding the SQL prepared statement with values
                 */
                cmd.Parameters.Add(new SqlParameter("bookid", Obj.BookID));
                cmd.Parameters.Add(new SqlParameter("studentid", Obj.StudentID));
                cmd.Parameters.Add(new SqlParameter("datetime", Obj.IssueDate));
                cmd.Parameters.Add(new SqlParameter("returndate", Obj.ReturnDate));
                cmd.Parameters.Add(new SqlParameter("status", Obj.Status));
                cmd.Parameters.Add(new SqlParameter("returnedon", Obj.ReturnedOn));
                cmd.Parameters.Add(new SqlParameter("amountCollected", Obj.PenaltyAmount));

                /*
                 * Opening sql connection
                 */
                Conn.Open();

                /*
                 * @return rows = number of rows affected
                 */
                rows = cmd.ExecuteNonQuery();
                UpdateBookIsIssue(Obj.BookID);
                UpdateStudentIssueCount(Obj.StudentID);
            }
            catch (Exception ex)
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

        void UpdateBookIsIssue(int BookID, int isIssued = 1)
        {
            string UpdateBook = "UPDATE books SET isIssued = @isIssued where id = @id";
            SqlCommand UpdateBookcmd = new SqlCommand(UpdateBook, Conn);
            UpdateBookcmd.Parameters.Add(new SqlParameter("id", BookID));
            UpdateBookcmd.Parameters.Add(new SqlParameter("isIssued", isIssued));

            UpdateBookcmd.ExecuteNonQuery();
        }
        
        void UpdateStudentIssueCount(int StdID, bool Decrement = false)
        {
            DataTable td = new DataTable();
            string sqlquery = "SELECT * FROM student WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sqlquery, Conn);
            cmd.Parameters.Add(new SqlParameter("id", StdID));
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(td);

            int Count;
            if (Decrement)
            {
                Count = Convert.ToInt32(td.Rows[0]["bookIssueCount"]) - 1;
            }
            else
            {
                Count = Convert.ToInt32(td.Rows[0]["bookIssueCount"]) + 1;
            }
            
            string updatequery = "UPDATE student SET bookIssueCount = @bookIssueCount where id = @id";
            SqlCommand Updatecmd = new SqlCommand(updatequery, Conn);
            Updatecmd.Parameters.Add(new SqlParameter("id", StdID));
            Updatecmd.Parameters.Add(new SqlParameter("bookIssueCount", Count));

            Updatecmd.ExecuteNonQuery();
        }

        internal bool UpdateIssue(int IssueID)
        {
            bool status = false;
            DataTable td = new DataTable();
            try
            {
                Conn.Open();

                string FetchQuery = "SELECT * FROM issue WHERE issueid = @issueid";

                SqlCommand cmd = new SqlCommand(FetchQuery, Conn);
                cmd.Parameters.Add(new SqlParameter("issueid", IssueID));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                
                adp.Fill(td);

                int BookID = Convert.ToInt32(td.Rows[0]["bookid"]);
                int StdID = Convert.ToInt32(td.Rows[0]["studentid"]);
                DateTime ReturnDate = Convert.ToDateTime(td.Rows[0]["returndate"]);
                DateTime CurrentDate = DateTime.Now;

                double PenaltyAmt = 0;
                int Cmpresult = DateTime.Compare(CurrentDate, ReturnDate);
                if (Cmpresult > 0)
                {
                    double ExceededDays = (CurrentDate - ReturnDate).TotalDays;
                    PenaltyAmt = (int)ExceededDays * 5;
                }

                string UpdateQuery = "UPDATE issue SET status = 1, returnedon = @returnedon, amountCollected = @amountCollected WHERE issueid = @issueid";
                SqlCommand Updatecmd = new SqlCommand(UpdateQuery, Conn);

                Updatecmd.Parameters.Add(new SqlParameter("issueid", IssueID));
                Updatecmd.Parameters.Add(new SqlParameter("returnedon", CurrentDate));
                Updatecmd.Parameters.Add(new SqlParameter("amountCollected", PenaltyAmt));

                int rows = Updatecmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    UpdateBookIsIssue(BookID, 0);
                    UpdateStudentIssueCount(StdID, true);
                    status = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return status;
        }

    }
}