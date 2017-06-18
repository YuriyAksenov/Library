using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.BusinessLayer
{
    /// <summary>
    /// Provides the instance of Subscriber
    /// </summary>
    public class Subscriber
    {
        /// <summary>
        /// List of the taken books
        /// </summary>
        private List<Book> Books { get; }

        /// <summary>
        /// Name of the subscriber
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Phone of the subscriber
        /// </summary>
        public string Phone { get; }

        public Subscriber(string name, string phone)
        {
            this.Name = name;
            this.Phone = phone;

            Books = new List<Book>();
        }

        /// <summary>
        /// Adds book into the taken books
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(Book book)
        {
            this.Books.Add(book);
        }

        /// <summary>
        /// REmoves book from the taken books
        /// </summary>
        /// <param name="book"></param>
        public void RemoveBook(Book book)
        {
            this.Books.Remove(book);
        }

        /// <summary>
        /// Returns the bunch of the take books of the subscriber
        /// </summary>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> GetTakenBooks()
        {
            return this.Books;
        }

        /// <summary>
        /// Returns the overdue books of the subscriber
        /// </summary>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> GetOverdueTakenBooks()
        {
            return this.Books.Where(x => ((DateTime.Now - x.IssueDate).Days >= 14));
        }

        /// <summary>
        /// Gets the rare books
        /// </summary>
        /// <returns>IEnumerableBook</returns>
        public IEnumerable<Book> GetRareBooks()
        {
            return this.Books.Where(x => (x.Rare));
        }

        /// <summary>
        /// Determines whether the specified Subscriber is equal to the current Subscriber.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            if(obj is Subscriber)
            return this.Name == ((Subscriber)obj).Name && this.Phone == ((Subscriber)obj).Phone;

            return false;
        }

        public override string ToString()
        {
            return $"Name: {this.Name,-5} Title: {this.Phone,-5}";
        }
    }
}
