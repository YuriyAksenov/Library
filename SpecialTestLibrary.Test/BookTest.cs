using LibraryApp.BusinessLayer;
using SpecialTestLibrary.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialTestLibrary.Test
{
    [TestClass]
    public class BookTest
    {
        private Subscriber _subscriber;
        private Book _book;

        [PrelaunchExecution]
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

        [TestMethod]
        public void GetBookLocationTest()
        {
            
            Test.AreEqual(BookLocation.Library,this._book.BookLocation);

            _book.Subscribe(this._subscriber);

            Test.AreEqual(BookLocation.Subscriber, _book.BookLocation);
        }


        [TestMethod]
        public void SubscribeTest()
        {
            DateTime dateTime = new DateTime(2000,01,01);

            _book.Subscribe(this._subscriber, dateTime);

            Test.AreEqual(dateTime, _book.IssueDate);

            Test.AreEqual(this._subscriber, this._book.BookSubscriber);
        }

        [TestMethod]
        public void UnsubscribeTest()
        {
            this._book.Subscribe(this._subscriber);
            this._book.Unsubscribe();
            
            Test.Null(this._book.BookSubscriber);
        }

        [TestMethod]
        public void BookToStringTest()
        {
            
            Test.AreEqual($"Author: {this._book.Author,-5} Title: {this._book.Title,-5}  Frequent", this._book.ToString());
        }
        

    }
}
