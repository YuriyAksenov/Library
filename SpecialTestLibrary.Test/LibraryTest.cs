﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.BusinessLayer;

namespace SpecialTestLibrary.Test
{
    [TestClass]
    class LibraryTest
    {
        private Library _library;

        [PrelaunchExecution]
        public void SetUp()
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

            this._library = new Library(books, subscribers);
        }

        [TestMethod]
        public void AddTheBookToTheLibraryTest()
        {
            Library library = new Library();
            Test.IsEmpty(library.Books);

            Book book = new Book("Гоголь", "98319516888");

            library.AddTheBookToTheLibrary(book);

            Test.AreEqual(book, library.Books.FirstOrDefault());
        }

        [TestMethod]
        public void RemoveTheBookFromTheLibrayTest()
        {
            Library library = new Library();

            Test.IsEmpty(library.Books);

            Book book = new Book("Гоголь", "98319516888");

            library.AddTheBookToTheLibrary(book);
            library.RemoveTheBookFromTheLibrary(book);

            Test.IsEmpty(library.Books);
        }

        [TestMethod]
        public void FindBooksByAuthorTest()
        {
            int k = 0;
            var findingBooks = this._library.FindBooksByAuthor("Толстой");
            var rightCountOfAuthorBooks = this._library.Books.Where(x => x.Author == "Толстой");

            foreach (var book in rightCountOfAuthorBooks)
            {
                if (findingBooks.Any(x => x.Author == book.Author && x.Title == book.Title && x.Rare == book.Rare)) k++;
            }

            Test.AreEqual(rightCountOfAuthorBooks.Count(), k);
        }

        [TestMethod]
        public void FindBooksByTitle()
        {
            int k = 0;
            var findingBooks = this._library.FindBooksByTitle("Детство");
            var rightCountOfTitleBooks = this._library.Books.Where(x => x.Title == "Детство");

            foreach (var book in rightCountOfTitleBooks)
            {
                if (findingBooks.Any(x => x.Author == book.Author && x.Title == book.Title && x.Rare == book.Rare)) k++;
            }

            Test.AreEqual(rightCountOfTitleBooks.Count(), k);
        }

        [TestMethod]
        public void FindTheSubscriberByNameTest()
        {
            int k = 0;
            var findingSubscribers = this._library.FindTheSubscriberByName("Михаил");
            var rightCountOfSubscribers = this._library.Subscribers.Where(x => x.Name == "Михаил");

            foreach (var subscriber in rightCountOfSubscribers)
            {
                if (rightCountOfSubscribers.Any(x => x.Name == subscriber.Name && x.Phone == subscriber.Phone)) k++;
            }

            Test.AreEqual(rightCountOfSubscribers.Count(), k);
        }

        [TestMethod]
        public void FindTheSubscriberByPhoneTest()
        {
            int k = 0;
            var findingSubscribers = this._library.FindTheSubscriberByPhone("89996687956");
            var rightCountOfSubscribers = this._library.Subscribers.Where(x => x.Phone == "89996687956");

            foreach (var subscriber in rightCountOfSubscribers)
            {
                if (rightCountOfSubscribers.Any(x => x.Name == subscriber.Name && x.Phone == subscriber.Phone)) k++;
            }

            Test.AreEqual(rightCountOfSubscribers.Count(), k);
        }

        [TestMethod]
        public void SubscribeTheBookFromTheSubscriber()
        {
            var subscriber = this._library.Subscribers.FirstOrDefault();
            var book = this._library.Books.FirstOrDefault();

            this._library.SubscribeTheBookToTheSubscriber(subscriber, book);

            Test.AreEqual(book, subscriber.GetTakenBooks().FirstOrDefault());
        }

        [TestMethod]
        public void UnsubscribeAListOfBooksInTheLibraryTest()
        {
            var subscriber = this._library.Subscribers.FirstOrDefault();
            var book = this._library.Books.FirstOrDefault();

            this._library.SubscribeTheBookToTheSubscriber(subscriber, book);

            this._library.UnsubscribeTheBookFromTheSubscriber(subscriber, book);

            Test.IsEmpty(subscriber.GetTakenBooks());
        }
    
        [TestMethod]
        public void GetAListOfBooksFromSubscribersTest()
        {
            var subscriber1 = this._library.Subscribers.Where(x=>x.Name=="Лев").FirstOrDefault();
            var subscriber2 = this._library.Subscribers.Where(x => x.Name == "Антон").FirstOrDefault();

            var book1 = this._library.Books.Where(x => x.Title == "Мертвые души").FirstOrDefault();
            var book2 = this._library.Books.Where(x => x.Title == "Гроза").FirstOrDefault();
            var book3 = this._library.Books.Where(x => x.Title == "Повесть временных лет").FirstOrDefault();

            List<Book> rightListOfBooks = new List<Book>()
            {
                book1,
                book2,
                book3
            };

            this._library.SubscribeTheBookToTheSubscriber(subscriber1,book1);
            this._library.SubscribeTheBookToTheSubscriber(subscriber1, book2);
            this._library.SubscribeTheBookToTheSubscriber(subscriber2, book3);

            var listOfBooks = this._library.GetAListOfBooksFromSubscribers();

            int k = 0;

            foreach (var book in rightListOfBooks)
            {
                if (listOfBooks.Any(x => x.Author == book.Author && x.Title == book.Title && x.Rare == book.Rare && x.BookLocation == BookLocation.Subscriber)) k++;
            }

            Test.AreEqual(rightListOfBooks.Count(), k);
        }

        

    }
}
