using LibraryApp.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationDeserialization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>()
            {
                new Book("Толстой","Война и мир", false),
                new Book("Толстой","Анна Каренина", false),
                new Book("Толстой","Детство", false),
                new Book("Толстой","Детство", false),
                new Book("Чехов","Вишневый сад", true),
                new Book("Гоголь","Мертвые души", false),
                new Book("Островский","Гроза", false),
                new Book("Неизвестно","Повесть временных лет", true),
                new Book("Пушкин","Руслан и Людмила", false),
                new Book("Лермонтов","Герой нашего времени", false),
                new Book("Кинг","Под куполом", false),
                new Book("Rouling", "Harry Potter", false)
            };

            List<Subscriber> subscribers = new List<Subscriber>()
            {
                new Subscriber("Михаил","89996687956"),
                new Subscriber("Михаил","89996683333"),
                new Subscriber("Лев","89996687957"),
                new Subscriber("Антон","89996687988"),
                new Subscriber("Николай","89996687999"),
                new Subscriber("Александр","89996687101"),
                new Subscriber("Стивен","89996687505"),
                new Subscriber("Наташа","89996687606"),
                new Subscriber("Нос","89996687707")
            };

            var library = new Library(books, subscribers);

            library.SaveToFile(library,"D:\\Кронштадт\\C#\\Library\\test.txt");
            //library.LoadFromFile("D:\\Кронштадт\\C#\\Library\\test.txt");
        }
    }
}
