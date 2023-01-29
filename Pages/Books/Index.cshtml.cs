using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BcasDigitalLibrary.Pages.Books
{
    public class IndexModel : PageModel
    {
        public List <BooksInfo> listBooks = new List<BooksInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=bcasdigitallibrary;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                { 
                    connection.Open();
                    String sql = "SELECT * FROM books";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BooksInfo booksInfo = new BooksInfo();
                                booksInfo.id = "" + reader.GetInt32(0);
                                booksInfo.name = reader.GetString(1);
                                booksInfo.author = reader.GetString(2);
                                booksInfo.publication = reader.GetString(3);
                                booksInfo.category = reader.GetString(4);
                                booksInfo.created_at = reader.GetDateTime(5).ToString();

                                listBooks.Add(booksInfo);
                            }
                        }
                    }
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    

    public class BooksInfo
    {
        public String id;
        public String name;
        public String author;
        public String publication;
        public String category;
        public String created_at;

    }
}
