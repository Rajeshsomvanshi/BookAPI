using Book_Management.DataModels;
using Book_Management.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpPost("add-book")]

        public async Task<string> AddBook(BookVM book)
        {
            try
            {
                await _bookService.AddBookAsync(book);
                return "Book Added Successfully";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        [HttpPost("search-book")]
        public IActionResult GetBooks(SearchBookVM searchBookVM)
        {
            try
            {
                
                return Ok(_bookService.SearchBook(searchBookVM));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-book/{bookID}")]
        public async Task<IActionResult> DeleBook(int bookID)
        {
            try
            {
                await _bookService.DeleteBookAsync(bookID);
                return Ok("Book Deleted Successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-book")]

        public async Task<IActionResult> UpdateBook(BookVM bookVM)
        {
            try
            {
                var book = await _bookService.UpdateBookAsync(bookVM);
                return Ok(book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-book")]

        public List<BookVM> GetAllBooks()
        {
            return _bookService.GetAllBooks();
        }

        [HttpGet("get-bookbyId/{id}")]

        public BookVM GetBookByID(int id)
        {
            try
            {
                return _bookService.GetBookByID(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
