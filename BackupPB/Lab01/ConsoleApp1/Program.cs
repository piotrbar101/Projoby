using Library;
using ClassLibrary1;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\piotr\\OneDrive\\Pulpit\\4 semestr\\Projektowanie obiektowe\\Lab01\\ConsoleApp1\\XMLFile1.xml";
            typeLibrary lib = LibraryReader.ReadLibrary(path);
            foreach (var cur_book in lib.book)
            {
                Console.WriteLine(cur_book.title);

                foreach (var aut in cur_book.author_ref)
                {
                    foreach (var aut_main in lib.author_main)
                        if (aut_main.id == aut.id)
                            Console.WriteLine(string.Join(", ", aut_main.name) + " " + aut_main.surname);
                }
            }
        }
    }
}