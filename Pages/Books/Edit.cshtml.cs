using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace BcasDigitalLibrary.Pages.Books
{
    public class EditModel : PageModel
    {
        public BooksInfo booksInfo = new BooksInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=bcasdigitallibrary;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM books WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())

                        {
                            if (reader.Read())

                            {
                                booksInfo.id = "" + reader.GetInt32(0);
                                booksInfo.name = reader.GetString(1);
                                booksInfo.author = reader.GetString(2);
                                booksInfo.publication = reader.GetString(3);
                                booksInfo.category = reader.GetString(4);

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            booksInfo.id = Request.Form["id"];
            booksInfo.name = Request.Form["name"];
            booksInfo.author = Request.Form["author"];
            booksInfo.publication = Request.Form["publication"];
            booksInfo.category = Request.Form["category"];

            if (booksInfo.name.Length == 0 || booksInfo.author.Length == 0 ||
                booksInfo.publication.Length == 0 || booksInfo.category.Length == 0)

            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=bcasdigitallibrary;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE books SET name=@name, author=@author, publication=@publication, category=@category WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", booksInfo.id);
                        command.Parameters.AddWithValue("@name", booksInfo.name);
                        command.Parameters.AddWithValue("@author", booksInfo.author);
                        command.Parameters.AddWithValue("@publication", booksInfo.publication);
                        command.Parameters.AddWithValue("@category", booksInfo.category);


                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Books/Index");
        }
    }
}
