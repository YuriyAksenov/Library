using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace LibraryApp.BusinessLayer
{
    [DataContract]
    public enum BookStateChanged
    {
        Subscribing,
        Unsubscribing
    }

    /// <summary>
    /// Represents errors that occur during unsubscribing.
    /// </summary>
    public class UnsubscribingException : Exception
    {
        public Subscriber subscriber { get; }
        public Book book { get; }

        public UnsubscribingException() : this("UnsubscribingException") { }
        public UnsubscribingException(string message) : this(message, null, null) { }
        public UnsubscribingException(string message, Subscriber subscriber, Book book) : base(message)
        {
            this.subscriber = subscriber;
            this.book = book;
        }
    }

    /// <summary>
    /// Represents errors that occur during Subscribing.
    /// </summary>
    public class SubscribingException : Exception
    {
        public Subscriber subscriber { get; }
        public Book book { get; }

        public SubscribingException() : this("SubscribingException") { }
        public SubscribingException(string message) : this(message, null, null) { }
        public SubscribingException(string message, Subscriber subscriber, Book book) : base(message)
        {
            this.subscriber = subscriber;
            this.book = book;
        }
    }


    public class AddBookEventArgs : EventArgs
    {
        public Book Book { get; }
        public bool IsExecuted { get; }
        public string Message { get; }

        public AddBookEventArgs(Book book, bool isExecuted, string message)
        {
            Book = book;
            IsExecuted = isExecuted;
            Message = message;
        }
    }

    public class AddSubscriberEventArgs : EventArgs
    {
        public Subscriber Subscriber { get; }
        public bool IsExecuted { get; }
        public string Message { get; }

        public AddSubscriberEventArgs(Subscriber subscriber, bool isExecuted, string message)
        {
            Subscriber = subscriber;
            IsExecuted = isExecuted;
            Message = message;
        }
    }

    public class BookStateChangedEventArgs : EventArgs
    {
        public Book Book { get; }
        public BookStateChanged BookStateChanged { get; }
        public bool IsExecuted { get; }
        public string Message { get; }

        public BookStateChangedEventArgs(Book book, BookStateChanged bookStateShanged, bool isExecuted, string message)
        {
            Book = book;
            BookStateChanged = bookStateShanged;
            IsExecuted = isExecuted;
            Message = message;
        }
    }

    /// <summary>
    /// Provides the instance of Library
    /// </summary>
    [DataContract]
    public class Library
    {
        public event EventHandler<AddBookEventArgs> AddBook;
        public event EventHandler<AddSubscriberEventArgs> AddSubscriber;
        public event EventHandler<BookStateChangedEventArgs> BookStateChanged;

        /// <summary>
        /// Returns the element at a specified index in a sequence.
        /// </summary>
        /// <param name="author"></param>
        /// <param name="title"></param>
        /// <returns>Book</returns>
        public Book this[string author, string title] => Books.Find(x => x.Author == author && x.Title == title);

        /// <summary>
        /// List of the books in the whole Library
        /// </summary>
        [DataMember]
        public List<Book> Books { get; }

        /// <summary>
        /// List of the subscriber in the whole Library
        /// </summary>
        [DataMember]
        public List<Subscriber> Subscribers { get; }

        public Library() : this(new List<Book>(), new List<Subscriber>()) { }

        public Library(List<Book> books, List<Subscriber> subscribers)
        {
            this.Books = books;
            this.Subscribers = subscribers;
        }

        #region Book 

        /// <summary>
        /// Create new instance of the book and add this into the list of books
        /// </summary>
        /// <param name="book"></param>
        public void AddTheBookToTheLibrary(Book book)
        {
            Books.Add(book);
            AddBook?.Invoke(this, new AddBookEventArgs(book, true, "Книга добавлена"));
        }

        /// <summary>
        /// Removes book from the list of the books
        /// </summary>
        /// <param name="book"></param>
        public void RemoveTheBookFromTheLibrary(Book book)
        {
            Subscriber subscriber = book.BookSubscriber;
            subscriber?.RemoveBook(book);
            Books.Remove(book);
        }

        /// <summary>
        /// Finds books by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Book> FindBooks(Func<Book, bool> predicate) => Books.Where(predicate);

        /// <summary>
        /// Finds subscriber sby predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Subscriber> FindSubscribers(Func<Subscriber, bool> predicate) => Subscribers.Where(predicate);

        /// <summary>
        /// Find and returns a bunch of books which author is contains transmitted value
        /// </summary>
        /// <param name="author"></param>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> FindBooksByAuthor(string author) => FindBooks(x => x.Author.Contains(author));

        /// <summary>
        /// Find and returns a bunch of books which title is contains transmitted value
        /// </summary>
        /// <param name="title"></param>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> FindBooksByTitle(string title) => FindBooks(x => x.Title.Contains(title));

        /// <summary>
        /// Returns IEnumerable of books which are located in library 
        /// </summary>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> GetAListOfBooksInTheLibrary() => Books.Where(x => x.BookLocation == BookLocation.Library);

        /// <summary>
        /// Returns IEnumerable of books which belong to subscribers
        /// </summary>
        /// <returns>IEnumerable Book</returns>
        public IEnumerable<Book> GetAListOfBooksFromSubscribers() => Books.Where(x => x.BookLocation == BookLocation.Subscriber);

        #endregion Book

        #region Subscriber 

        /// <summary>
        /// Create new instance of the subscriber and add this into the list of books
        /// </summary>
        /// <param name="subscriber"></param>
        public void AddTheSubscriberToTheLibrary(Subscriber subscriber)
        {
            Subscribers.Add(subscriber);
            AddSubscriber?.Invoke(this, new AddSubscriberEventArgs(subscriber, true, "Абонент добавлен"));
        }

        /// <summary>
        /// Removes subscriber from the list of the subscribers
        /// </summary>
        /// <param name="subscriber"></param>
        public void RemoveTheSubscriberFromTheLibrary(Subscriber subscriber)
        {
            IEnumerable<Book> removingBooks = subscriber.GetTakenBooks();
            foreach (var book in removingBooks)
            {
                subscriber.RemoveBook(book);
            }
            Subscribers.Remove(subscriber);
        }

        /// <summary>
        /// Finds subscriber by input name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Subscriber</returns>
        public IEnumerable<Subscriber> FindTheSubscriberByName(string name) => FindSubscribers(x => x.Name == name);

        /// <summary>
        /// Finds subscriber by a phone
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>Subscriber</returns>
        public IEnumerable<Subscriber> FindTheSubscriberByPhone(string phone) => FindSubscribers(x => x.Phone == phone);

        #endregion Subscriber

        #region Subscribing / Unsubscribing 
        /// <summary>
        /// Gives instance of book to the subscriber 
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="book"></param>
        public void SubscribeTheBookToTheSubscriber(Subscriber subscriber, Book book) => SubscribeTheBookToTheSubscriber(subscriber, book, DateTime.Now);

        /// <summary>
        /// Gives instance of book to the subscriber and sets a specific time
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="book"></param>
        public void SubscribeTheBookToTheSubscriber(Subscriber subscriber, Book book, DateTime subscribingTime)
        {
            if (subscriber == null) throw new ArgumentNullException("subsriber", "Sibscriber is null");
            if (book == null) throw new ArgumentNullException("book", "Book is null");
            if (subscriber.GetTakenBooks().Count() >= 5) throw new SubscribingException("У абонента превышен лимит книг", subscriber, book);
            if (subscriber.GetOverdueTakenBooks().Any()) throw new SubscribingException("У абонента есть просроченные книги", subscriber, book);
            if (book.BookSubscriber != null) throw new SubscribingException("Книга уже у другого абонента", subscriber, book);
            if (book.Rare && subscriber.GetRareBooks().Any()) throw new SubscribingException("У абонента уже есть редкая книга", subscriber, book);

            book.Subscribe(subscriber, subscribingTime);
            subscriber.AddBook(book);

            BookStateChanged?.Invoke(this, new BookStateChangedEventArgs(book, BusinessLayer.BookStateChanged.Subscribing, true, "Книга выдана абоненту."));

            //BookStateChanged?.Invoke(this, new BookStateChangedEventArgs(book, BusinessLayer.BookStateChanged.Subscribing, false, "Книга не выдана абоненту, так как редкая книга уже имеется у абонента."));
        }

        /// <summary>
        /// Gets instance of the book from the subscriber and indocated, that this book in library
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="book"></param>
        public void UnsubscribeTheBookFromTheSubscriber(Subscriber subscriber, Book book)
        {
            if (subscriber == null) throw new ArgumentNullException("subsriber", "Sibscriber is null");
            if (book == null) throw new ArgumentNullException("book", "Book is null");

            if (!subscriber.GetTakenBooks().Any(x => x == book))
            {
                //BookStateChanged?.Invoke(this, new BookStateChangedEventArgs(book, BusinessLayer.BookStateChanged.Unsubscribing, false, "Книга не принята от абонента"));
                throw new UnsubscribingException("Subscriber does not have trasmitted book in taken books", subscriber, book);
            }

            book.Unsubscribe();
            subscriber.RemoveBook(book);

            BookStateChanged?.Invoke(this, new BookStateChangedEventArgs(book, BusinessLayer.BookStateChanged.Unsubscribing, true, "Книга принята от абонента"));
        }

        #endregion Subscribing / Unsubscribing

        public Library LoadFromFile(string path)
        {
            var serializer = new DataContractSerializer(typeof(Library), null, 1000, false, true, null);
            
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var xmlReader = XmlReader.Create(fs))
                {
                    return (Library)serializer.ReadObject(xmlReader);
                }
            }
        }

        public void SaveToFile(Library library, string path)
        {
            //throw new NotImplementedException();
            var serializer  = new DataContractSerializer(typeof(Library), null, 1000, false, true, null);
            var xmlWriterSettings = new XmlWriterSettings { Indent = true };
            
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var xmlWriter = XmlWriter.Create(fs, xmlWriterSettings))
                {
                    serializer.WriteObject(xmlWriter, library);
                }
            }
            
        }

    }
}
