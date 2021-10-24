using Book_Management.DataModels;
using Book_Management.DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Book_Management.Service
{
    public class BookService : IBookService
    {
        private AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(BookVM bookVM)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var _book = new Book()
                {
                    ISBN = bookVM.ISBN,
                    Title = bookVM.Title,
                    PublicationDate = bookVM.PublicationDate
                };
                await _context.Books.AddAsync(_book);
                await _context.SaveChangesAsync();

                foreach (var authname in bookVM.Authors)
                {
                    var author = _context.Authors.Where(x => x.AuthorName == authname.ToLower()).FirstOrDefault();
                    if (author != null)
                    {
                        AddBookAuthor(bookVM.ISBN, author.AuthorId);
                    }
                    else
                    {
                        var newauth = new Author()
                        {
                            AuthorName = authname
                        };
                        await _context.Authors.AddAsync(newauth);
                        await _context.SaveChangesAsync();

                        AddBookAuthor(bookVM.ISBN, newauth.AuthorId);
                    }
                }
                transaction.Commit();

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void AddBookAuthor(int ISBN, int AuthID)
        {
            //int q = _context.Authors.Max(a => a.AuthorId);
            var book_author = new Book_Author()
            {
                BookId = ISBN,
                Id = AuthID
            };
             _context.Book_Authors.Add(book_author);
             _context.SaveChanges();
        }


        public List<BookVM> SearchBook(SearchBookVM searchBookVM)
        {


            List<BookVM> bookVMs = new List<BookVM>();
            if (searchBookVM.ISBN >0 || !String.IsNullOrEmpty(searchBookVM.Title))
            {

                var book = _context.Books.Where(z => z.ISBN == searchBookVM.ISBN || z.Title == searchBookVM.Title).FirstOrDefault();
                var book_author = _context.Book_Authors.Where(x => x.BookId == book.ISBN).ToList();


                var auth = (from a in _context.Authors
                            join ba in _context.Book_Authors on a.AuthorId equals ba.Id
                            where ba.BookId == book.ISBN
                            select a.AuthorName).ToList();

                BookVM bookVM = new BookVM()
                {
                    ISBN = book.ISBN,
                    Title = book.Title,
                    PublicationDate = book.PublicationDate,
                    Authors = auth
                };

                bookVMs.Add(bookVM);
                return bookVMs;
            }
            else if (!String.IsNullOrEmpty(searchBookVM.AuthorName))
            {
                var author = _context.Authors.Where(x => x.AuthorName == searchBookVM.AuthorName).FirstOrDefault();
                if(author != null)
                {
                    var book = from b in _context.Books
                               join ba in _context.Book_Authors on b.ISBN equals ba.BookId
                               where ba.Id == author.AuthorId
                               select b;

                    foreach(Book book1 in book)
                    {
                        BookVM bookVM = new BookVM()
                        {
                            ISBN = book1.ISBN,
                            Title = book1.Title,
                            PublicationDate = book1.PublicationDate,
                            Authors = new List<string>()
                            {
                                author.AuthorName
                            }
                        };
                        bookVMs.Add(bookVM);
                    }
                    return bookVMs;
                }
                else
                {
                    throw new Exception("Author not found");
                }
                
            }
            else
            {
                throw new Exception("No books found");
            }
        }


        public async Task DeleteBookAsync(int ISBN)
        {
            try
            {   var book = _context.Books.Where(x => x.ISBN == ISBN).FirstOrDefault();
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookVM> UpdateBookAsync(BookVM book)
        {
            var existing_book = _context.Books.Where(x => x.ISBN == book.ISBN).FirstOrDefault();
            if(existing_book != null)
            {
                existing_book.Title = book.Title;
                existing_book.PublicationDate = book.PublicationDate;
                await _context.SaveChangesAsync();
                return new BookVM()
                {
                    ISBN = book.ISBN,
                    Title = existing_book.Title,
                    PublicationDate = existing_book.PublicationDate,
                    Authors = book.Authors
                };
            }
            else
            {
                throw new Exception("No Book Found");
            }
        }

        public List<BookVM> GetAllBooks()
        {
            List<BookVM> bookVMs = new List<BookVM>();
            var book = (from s in _context.Books
                        select s).ToList();


            
            foreach(var b in book)
            {
                var auth = (from a in _context.Authors
                            join ba in _context.Book_Authors on a.AuthorId equals ba.Id
                            where ba.BookId==b.ISBN
                            select a.AuthorName).ToList();
                BookVM bookVM = new BookVM()
                {
                    ISBN = b.ISBN,
                    Title = b.Title,
                    PublicationDate = b.PublicationDate,
                    Authors = auth
                };
                bookVMs.Add(bookVM);
            }
            return bookVMs;
        }

        public BookVM GetBookByID(int id)
        {
            var book = _context.Books.Where(x => x.ISBN == id).FirstOrDefault();
            var auth = (from a in _context.Authors
                        join ba in _context.Book_Authors on a.AuthorId equals ba.Id
                        where ba.BookId == book.ISBN
                        select a.AuthorName).ToList();
            BookVM bookVM = new BookVM()
            {
                ISBN = book.ISBN,
                Title = book.Title,
                PublicationDate = book.PublicationDate,
                Authors = auth
            };

            return bookVM;
        }
    }
}
