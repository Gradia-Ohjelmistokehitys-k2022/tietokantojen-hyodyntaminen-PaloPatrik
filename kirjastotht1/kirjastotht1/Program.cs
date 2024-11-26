using kirjastotht1.Models;

namespace kirjastotht1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var db = new DataBaseRepository("Trusted_Connection=true;" +
                "server=(localdb)\\MSSQLLocalDB;" +
                "database=Library;");
            Console.WriteLine(db.IsDbConnectionEstablished());


            foreach (var book in db.GetAllBooks()) {
                Console.WriteLine("- "+book.Title);
                Console.WriteLine("\t- "+book.ISBN);
                Console.WriteLine("\t- "+book.PublicationYear);
                Console.WriteLine("\t- "+book.AvailableCopies);
            }

            /*foreach(var member in db.GetAllUsers())
            {
                Console.WriteLine(member.Age);
            }*/

            Console.WriteLine(db.GetAverageAge().ToString());

            Console.WriteLine(db.MostAvailable().Title);

            db.PrintMembersWhoBorrowedBooks();
        }
    }
}
