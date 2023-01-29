using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace BcasDigitalLibrary.Pages.Books
{
    public class CreateModel : PageModel
    {
        public BooksInfo booksInfo = new BooksInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
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

            //save the new book into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=bcasdigitallibrary;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO books " +
                                 "(name, author, publication, category) VALUES " +
                                 "(@name, @author, @publication, @category);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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
                errorMessage= ex.Message;
                return;
            }

            booksInfo.name = ""; booksInfo.author = ""; booksInfo.publication = ""; booksInfo.category = "";
            successMessage = "New Book Added Correctly";

            Response.Redirect("/Books/Index");


        }
    }
}
