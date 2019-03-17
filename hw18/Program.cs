using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace hw18
{
    class Program
    {
        
        static void Main(string[] args)
        {
             
        List<Book> books = new List<Book>
            {
               new Book { Name = "Тарас Бульба", Author = "Н.Гоголь", Price = 1111, Year = 1835 },
               new Book { Name = "Ася", Author = "И.Тургенев", Price = 2222, Year = 1858 },
               new Book { Name = "Руслан и Людмила", Author = "А.Пушкин", Price = 3333, Year = 1820 },
            };

            List<Book> books2 = new List<Book>();

            var serializer = new BinaryFormatter();

            using (var stream = File.Create("data.bin"))
            {
                serializer.Serialize(stream, books);
            }

            using (var stream = File.OpenRead("data.bin"))
            {
                books2 = serializer.Deserialize(stream) as List<Book>;          
            }
            //foreach (Book book in books2)
            //{
            //    Console.WriteLine($"Книгу \"{book.Name}\" написал {book.Author} в {book.Year}, продам за {book.Price}");
            //}

            foreach(var book in books2)
            {
                foreach (var property in book.GetType().GetRuntimeProperties())
                {
                    foreach (var attribute in property.GetCustomAttributes())
                    {
                        Console.Write(((MyAttribute)attribute).Name + property.GetValue(book));
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();          
        }
    }
}
