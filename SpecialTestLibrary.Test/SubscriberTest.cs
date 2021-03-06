﻿using LibraryApp.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialTestLibrary.Test
{
    [TestClass]
    public class SubscriberTest
    {
        private Book _book;
        private Subscriber _subscriber;

        [PrelaunchExecution]
        public void SetUp()
        {
            this._subscriber = new Subscriber("Иван", "89889996535");
            this._book = new Book("Пушкин", "Онегин", false);
        }

        [TestMethod]
        public void AddBookTest()
        {      
            Test.IsEmpty(this._subscriber.GetTakenBooks());         
            this._subscriber.AddBook(this._book);
            Test.AreEqual(this._subscriber.GetTakenBooks().Count(), 1);
            Test.AreEqual(this._subscriber.GetTakenBooks().FirstOrDefault(), this._book);
        }

        [TestMethod]
        public void RemoveBookTest()
        {
            Test.IsEmpty(this._subscriber.GetTakenBooks());
            this._subscriber.AddBook(_book);
            this._subscriber.RemoveBook(this._book);
            Test.IsEmpty(this._subscriber.GetTakenBooks());
        }

        [TestMethod]
        public void GetOverdueBooksTest()
        {
            DateTime dateTime = DateTime.Now.AddDays(-20);
            this._book.Subscribe(this._subscriber,dateTime);
            this._subscriber.AddBook(this._book);

            Test.AreEqual(this._book, this._subscriber.GetOverdueTakenBooks().FirstOrDefault());
        }

        [TestMethod]
        public void GetTakenBooksTest()
        {
            List<Book> books = new List<Book>()
            {
                new Book("Толстой","Война и мир"),
                new Book("Чехов","Вишневый сад", true),
                new Book("Гоголь","Мертвые души"),
                new Book("Островский","Гроза"),
                new Book("Неизвестно","Повесть временных лет", true),
            };

            books.ForEach(x => this._subscriber.AddBook(x));
            int k = 0;
            var takenBooks = this._subscriber.GetTakenBooks();

            foreach (var book in books)
            {
                if (takenBooks.Any(x=>x.Author == book.Author && x.Title == book.Title && x.Rare == book.Rare)) k++;
            }

            Test.AreEqual(books.Count,k);
        }

        [TestMethod]
        public void GetRareBooksTest()
        {
            List<Book> books = new List<Book>()
            {
                new Book("Толстой","Война и мир"),
                new Book("Чехов","Вишневый сад", true),
                new Book("Гоголь","Мертвые души"),
                new Book("Островский","Гроза"),
                new Book("Неизвестно","Повесть временных лет", true),
            };

            books.ForEach(x => this._subscriber.AddBook(x));
            int k = 0;
            var takenBooks = this._subscriber.GetRareBooks();

            foreach (var book in books.Where(x=>x.Rare))
            {
                if (takenBooks.Any(x => x.Author == book.Author && x.Title == book.Title && x.Rare == book.Rare)) k++;
            }

            Test.AreEqual(books.Where(x=>x.Rare).Count(), k);
        }

        [TestMethod]
        public void SubscriberToStringTest()
        {
            Test.AreEqual($"Name: {this._subscriber.Name,-5} Title: {this._subscriber.Phone,-5}",this._subscriber.ToString());
        }


        
    }

 
}
