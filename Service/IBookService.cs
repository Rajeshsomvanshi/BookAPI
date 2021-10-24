using Book_Management.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Management.Service
{
    public interface IBookService
    {
        Task AddBookAsync(BookVM book);
        List<BookVM> SearchBook(SearchBookVM searchBookVM);
        Task DeleteBookAsync(int ISBN);

        Task<BookVM> UpdateBookAsync(BookVM book);

        List<BookVM> GetAllBooks();

        BookVM GetBookByID(int id);
    }
}
