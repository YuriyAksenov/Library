using System;

namespace LibraryApp.BusinessLayer
{
    /// <summary>
    /// Provides the instance of book
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Author of the book
        /// </summary>
        public string Author { get; }
        /// <summary>
        /// Title of the book
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// Mark of the rare book
        /// </summary>
        public bool Rare { get; }


        /// <summary>
        /// The location of the book
        /// </summary>
        public BookLocation BookLocation
        {
            get
            {
                return BookSubscriber == null ? BookLocation.Library : BookLocation.Subscriber;
            }
        }

        public bool ComparingTheMainParametersOfTheBook(Book book)
        {
            return this.Author == book.Author && this.Title == book.Title && this.Rare == book.Rare;
        }

        /// <summary>
        /// Date of the issue book
        /// </summary>
        public DateTime IssueDate { get; private set; }
        /// <summary>
        /// Represents the instance of the owner of the book
        /// </summary>
        public Subscriber BookSubscriber { get; private set; } = null;

        /// <summary>
        /// Initialize a new instance of the book class
        /// </summary>
        /// <param name="author"></param>
        /// <param name="title"></param>
        /// <param name="rare"></param>
        public Book(string author, string title, bool rare = false)
        {
            this.Author = author;
            this.Title = title;
            this.Rare = rare;
        }

        /// <summary>
        /// Subscribes to this book
        /// </summary>
        /// <param name="subscriber"></param>
        public void Subscribe(Subscriber subscriber) 
        {
            this.Subscribe(subscriber, DateTime.Now);
        }

        /// <summary>
        /// Subscribes to this book and sets a specific time
        /// </summary>
        /// <param name="subscriber"></param>
        public void Subscribe(Subscriber subscriber, DateTime datetime)
        {
            this.BookSubscriber = subscriber;
            this.IssueDate = datetime;
        }

        /// <summary>
        /// Unsubscriber from this book
        /// </summary>
        public void Unsubscribe()
        {
            this.BookSubscriber = null;
            this.IssueDate = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            if (obj is Book)
                return this.Author == ((Book)obj).Author && this.Title == ((Book)obj).Title && this.Rare == ((Book)obj).Rare && this.BookLocation == ((Book)obj).BookLocation && this.BookSubscriber == ((Book)obj).BookSubscriber && this.IssueDate == ((Book)obj).IssueDate;

            return false;
        }

        public override int GetHashCode()
        {
            return Author.GetHashCode() ^ Title.GetHashCode();
        }

        public override string ToString()
        {
            string rare = this.Rare ? "Rare" : "Frequent";
            return $"Author: {this.Author,-5} Title: {this.Title,-5}  {rare}";
        }
    }


    public enum BookLocation
    {
        Library = 1,
        Subscriber = 2
    }

}
