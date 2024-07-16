using LibraryAPI.Contracts;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBooksManager _booksManager;
        private readonly IMyApiClient _myApiClient;


        public BooksController(ILogger<BooksController> logger, IBooksManager booksManager, IMyApiClient myApiClient)
        {
            _booksManager = booksManager;
            _logger = logger;
            _myApiClient = myApiClient;
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

        [HttpGet("open-library")]
        public async Task<IActionResult> GetBookOnline(string Book_Name)
        {

            var response = await _myApiClient.GetBookAsync(Book_Name);

            var byteArray = await response.Content.ReadAsByteArrayAsync();
            var content = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            return Ok(content);

        }


    }
}
