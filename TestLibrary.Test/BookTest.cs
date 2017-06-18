using LibraryApp.BusinessLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary.Test
{
    [TestFixture]
    public class BookTest
    {
        private Subscriber _subscriber;
        private Book _book;

        [SetUp]
        public void SetUp()
        {
            string name = "Иван";
            string phone = "83919799988";
            this._subscriber = new Subscriber(name, phone);

            string author = "Пушкин";
            string title = "Онегин";
            bool rare = false;
            this._book = new Book(author, title, rare);

        }

        [Test]
        public void GetBookLocationTest()
        {
            
            Assert.AreEqual(BookLocation.Library,this._book.BookLocation);

            _book.Subscribe(this._subscriber);

            Assert.AreEqual(BookLocation.Subscriber, _book.BookLocation);
        }


        [Test]
        public void SubscribeTest()
        {
            DateTime dateTime = new DateTime(2000,01,01);

            _book.Subscribe(this._subscriber, dateTime);

            Assert.AreEqual(dateTime, _book.IssueDate);

            Assert.AreEqual(this._subscriber, this._book.BookSubscriber);
        }

        [Test]
        public void UnsubscribeTest()
        {
            this._book.Subscribe(this._subscriber);
            this._book.Unsubscribe();
            
            Assert.Null(this._book.BookSubscriber);
        }

        [Test]
        public void BookToStringTest()
        {
            
            Assert.AreEqual($"Author: {this._book.Author,-5} Title: {this._book.Title,-5}  Frequent", this._book.ToString());
        }
        

    }
}
