using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.BusinessLayer
{
    /// <summary>
    /// Provides the instance of Library
    /// </summary>
    public class Library
    {
        /// <summary>
        /// List of the books in the whole Library
        /// </summary>
        public List<Book> Books { get; }

        /// <summary>
        /// List of the subscriber in the whole Library
        /// </summary>
        public List<Subscriber> Subscribers { get; }

        public Library(): this(new List<Book>(), new List<Subscriber>()) {}

        public Library(List<Book> books, List<Subscriber> subscribers)
        {
            this.Books = books;
            this.Subscribers = subscribers;
        }

        /// <summary>
        /// Create new instance of the book and add this into the list of books
        /// </summary>
        /// <param name="book"></param>
        public void AddTheBookToTheLibrary(Book book)
        {
            Books.Add(book);
        }

        /// <summary>
        /// Removes book from the list of the books
        /// </summary>
        /// <param name="book"></param>
        public void RemoveTheBookFromTheLibrary(Book book)
        {
            Books.Remove(book);
        }

        /// <summary>
        /// Finds books by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Book> FindBooks(Func<Book,bool> predicate)
        {
            return this.Books.Where(predicate);
        }

        /// <summary>
        /// Finds subscriber sby predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Subscriber> FindSubscribers(Func<Subscriber,bool> predicate)
        {
            return this.Subscribers.Where(predicate);
        }

        /// <summary>
        /// Find and returns a bunch of books which author is contains transmitted value
        /// </summary>
        /// <param name="author"></param>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> FindBooksByAuthor(string author)
        {
            return this.FindBooks(x => x.Author.Contains(author));
        }

        /// <summary>
        /// Find and returns a bunch of books which title is contains transmitted value
        /// </summary>
        /// <param name="title"></param>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> FindBooksByTitle(string title)
        {
            return this.FindBooks(x => x.Title.Contains(title));
        }

        /// <summary>
        /// Finds subscriber by input name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Subscriber</returns>
        public IEnumerable<Subscriber> FindTheSubscriberByName(string name)
        {
            return this.FindSubscribers(x => x.Name == name);
        }

        /// <summary>
        /// Finds subscriber by a phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>Subscriber</returns>
        public IEnumerable<Subscriber> FindTheSubscriberByPhone(string phone)
        {
            return this.FindSubscribers(x => x.Phone == phone);
        }

        /// <summary>
        /// Gives instance of book to the subscriber 
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="book"></param>
        public void SubscribeTheBookToTheSubscriber(Subscriber subscriber, Book book)
        {
            this.SubscribeTheBookToTheSubscriber(subscriber, book, DateTime.Now);
        }

        /// <summary>
        /// Gives instance of book to the subscriber and sets a specific time
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="book"></param>
        public void SubscribeTheBookToTheSubscriber(Subscriber subscriber, Book book, DateTime subscribingTime)
        {
            if (subscriber.GetTakenBooks().Count() < 5 && subscriber.GetRareBooks().Count() <= 1 && subscriber.GetOverdueTakenBooks().Count() < 1 && (book.BookSubscriber == null))
            {
                book.Subscribe(subscriber,subscribingTime);
                subscriber.AddBook(book);
            }
        }

        /// <summary>
        /// Gets instance of the book from the subscriber and indocated, that this book in library
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="book"></param>
        public void UnsubscribeTheBookFromTheSubscriber(Subscriber subscriber, Book book)
        {
            if (!subscriber.GetTakenBooks().Any(x => x == book)) return;

            book.Unsubscribe();
            subscriber.RemoveBook(book);
        }

        /// <summary>
        /// Returns IEnumerable of books which are located in library 
        /// </summary>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> GetAListOfBooksInTheLibrary()
        {
            return Books.Where(x => x.BookLocation == BookLocation.Library);
        }

        /// <summary>
        /// Returns IEnumerable of books which belong to subscribers
        /// </summary>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> GetAListOfBooksFromSubscribers()
        {
            return this.Books.Where(x => x.BookLocation == BookLocation.Subscriber);
        }

    }
}
