using LibraryAPI.Contracts;
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
        public async Task<IActionResult> GetAllBooks([FromHeader] string authorisation) => await _booksManager.GetAllBooks();

        [HttpGet("{id:int}")]
        public async Task<Book> GetBook([FromHeader] string authorisation, int id) => await _booksManager.GetBook(id);

        [HttpPost]
        public async Task<IActionResult> AddBook([FromHeader] string authorisation, IFormFile file)
        {
            await _booksManager.AddBook(file);
            return Ok("Books added!");
        }

        [HttpPut]
        public async Task<int> UpdateBook([FromHeader] string authorisation, Book book) => await _booksManager.UpdateBook(book);

        [HttpDelete("{id}")]
        public async Task<int> RemoveBook([FromHeader] string authorisation, int id) => await _booksManager.RemoveBook(id);


    }
}
