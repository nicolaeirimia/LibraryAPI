using LibraryAPI.Contracts;
using LibraryAPI.Filters;
using LibraryAPI.Managers;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly IAuthorsManager _authorsManager;
        public AuthorsController(ILogger<AuthorsController> logger, IAuthorsManager authorsManager)
        {
            _logger = logger;
            _authorsManager = authorsManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors() => await _authorsManager.GetAllAuthors();

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        public async Task<Author> GetAuthor(int id) => await _authorsManager.GetAuthor(id);

        [HttpPost]
        public async Task<IActionResult> AddAuthor(IFormFile file)
        {
            await _authorsManager.AddAuthor(file);
            return Ok("Authors added!");
        }

        [HttpDelete("{id}")]
        public async Task<int> RemoveAuthor(int id) => await _authorsManager.RemoveAuthor(id);

        [HttpPut]
        [Consumes("application/json")]
        public async Task<int> UpdateAutor(Author author) => await _authorsManager.UpdateAuthor(author);
    }
}
