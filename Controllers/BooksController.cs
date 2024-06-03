using LibraryAPI.Contracts;
using LibraryAPI.Filters;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBooksManager _booksManager;

        public BooksController(ILogger<BooksController> logger, IBooksManager booksManager)
        {
            _booksManager = booksManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks(string type) => await _booksManager.GetAllBooks(type);

        [HttpGet("{id:int}")]

        public async Task<Book> GetBook(int id) => await _booksManager.GetBook(id);

        [HttpPost]
        public async Task<IActionResult> AddBook(IFormFile file)
        {
            await _booksManager.AddBook(file);
            return Ok("Books added!");
        }

        [HttpPut]
        public async Task<int> UpdateBook(Book book) => await _booksManager.UpdateBook(book);

        [HttpDelete("{id}")]
        public async Task<int> RemoveBook(int id) => await _booksManager.RemoveBook(id);


    }
}
