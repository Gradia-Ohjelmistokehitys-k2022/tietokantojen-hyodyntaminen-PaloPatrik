using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace kirjastotht1.Models
{
    internal class DataBaseRepository
    {
        private string _connectionString;
        public DataBaseRepository(string connectionString) {
            _connectionString = connectionString;
        }

        [Obsolete]
        public string IsDbConnectionEstablished()
        {
            using var connection = new SqlConnection(_connectionString);

            try
            {
                connection.Open();
                return "Connection established!";
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }
    

      public List<Book> GetAllBooks()
        {
            List<Book> Books = new();

            using var dbConnection = new SqlConnection(_connectionString); // uusi tapa käyttää using-ominaisuutta. Tämä huolehtii yhteyden sulkemisesta.

            dbConnection.Open(); //avataan yhteys tietokantaan

            using var command = new SqlCommand("SELECT * FROM Book where PublicationYear > 2019", dbConnection); // kysely ja tietokannan osoite
            using var reader = command.ExecuteReader(); // olio, jolla luetaan tietoja kannasta
            while (reader.Read()) // silmukka, joka lukee kantaa niin kauan kuin siellä on rivejä, joital lukea
            {
                Book book = new() // jokaiselle riville luodaan uusi olio, johon tiedot tallennetaan
                {
                    Id = Convert.ToInt32(reader["BookId"]),
                    Title = reader["Title"].ToString(),
                    ISBN = reader["ISBN"].ToString(),
                    PublicationYear = Convert.ToInt32(reader["PublicationYear"]),
                    AvailableCopies = Convert.ToInt32(reader["AvailableCopies"])
                };
                Books.Add(book);
            }


            return Books;
        }

        public double GetAverageAge()
        {
            double averageAge = 0;

            using var dbConnection = new SqlConnection(_connectionString); 
            dbConnection.Open(); 

            
            using var command = new SqlCommand("SELECT AVG(Age) FROM Member", dbConnection);
            var result = command.ExecuteScalar(); 

            
            if (result != DBNull.Value)
            {
                averageAge = Convert.ToDouble(result); 
            }

            return averageAge; 
        }

        public Book MostAvailable()
        {
            Book book = null;
            using var dbConnection = new SqlConnection(_connectionString); 
            dbConnection.Open(); 

            
            string query = @"
        SELECT TOP 1 BookId, Title, AvailableCopies
        FROM Book
        ORDER BY AvailableCopies DESC";  

            
            using var command = new SqlCommand(query, dbConnection);
            using var reader = command.ExecuteReader(); 

            
            if (reader.Read())
            {
                book = new Book
                {
                    Id = reader.GetInt32(reader.GetOrdinal("BookId")),  
                    Title = reader.GetString(reader.GetOrdinal("Title")),   
                    AvailableCopies = reader.GetInt32(reader.GetOrdinal("AvailableCopies")) 
                };
            }

            return book;
        }



        public void PrintMembersWhoBorrowedBooks()
        {
            using var dbConnection = new SqlConnection(_connectionString); 
            dbConnection.Open(); 

             
            string query = @"
        SELECT m.FirstName, m.LastName, b.ISBN
        FROM Member m
        INNER JOIN Loan l ON m.MemberId = l.MemberId
        INNER JOIN Book b ON l.BookId = b.BookId
    ";

            using var command = new SqlCommand(query, dbConnection);
            using var reader = command.ExecuteReader(); 

            while (reader.Read())
            {
                string firstName = reader["FirstName"].ToString();
                string lastName = reader["LastName"].ToString();
                string isbn = reader["ISBN"].ToString();

                
                Console.WriteLine($"Member: {firstName} {lastName}, Borrowed Book ISBN: {isbn}");
            }
        }
         


    }
}




