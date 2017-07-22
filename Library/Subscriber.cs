using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace LibraryApp.BusinessLayer
{
    /// <summary>
    /// Provides the instance of Subscriber
    /// </summary>
    [DataContract]
    public class Subscriber
    {

        /// <summary>
        /// Returns the element at a specified index in a sequence.
        /// </summary>
        /// <param name="bookPosition"></param>
        /// <returns>Book</returns>
        public Book this[int bookPosition] => Books.ElementAt(bookPosition);

        /// <summary>
        /// List of the taken books
        /// </summary>
        [DataMember]
        private List<Book> Books { get; set; }

        /// <summary>
        /// Name of the subscriber
        /// </summary>
        [DataMember]
        public string Name { get; private set; }

        /// <summary>
        /// Phone of the subscriber
        /// </summary>
        [DataMember]
        public string Phone { get; private set; }

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
        public void AddBook(Book book) => Books.Add(book);

        /// <summary>
        /// REmoves book from the taken books
        /// </summary>
        /// <param name="book"></param>
        public void RemoveBook(Book book) => Books.Remove(book);

        /// <summary>
        /// Returns the bunch of the take books of the subscriber
        /// </summary>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> GetTakenBooks() => Books;

        /// <summary>
        /// Returns the overdue books of the subscriber
        /// </summary>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> GetOverdueTakenBooks() => Books.Where(x => ((DateTime.Now - x.IssueDate).Days >= 14));
        

        /// <summary>
        /// Gets the rare books
        /// </summary>
        /// <returns>IEnumerableBook</returns>
        public IEnumerable<Book> GetRareBooks() => Books.Where(x => (x.Rare));

        protected bool Equals(Subscriber other) => string.Equals(Name, other.Name) && string.Equals(Phone, other.Phone);


        /// <summary>
        /// Determines whether the specified Subscriber is equal to the current Subscriber.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Subscriber)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Phone != null ? Phone.GetHashCode() : 0);
            }
        }

        public override string ToString() => $"Name: {this.Name,-5} Title: {this.Phone,-5}";
    }
}
