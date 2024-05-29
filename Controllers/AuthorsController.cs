using LibraryAPI.Contracts;
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
        public async Task<IActionResult> GetAllAuthors([FromHeader] string authorisation) =>await _authorsManager.GetAllAuthors();

        [HttpGet("{id:int}")]
        public async Task<Author> GetAuthor([FromHeader] string authorisation, int id) => await _authorsManager.GetAuthor(id);

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromHeader] string authorisation, IFormFile file)
        {
            await _authorsManager.AddAuthor(file);
            return Ok("Books added!");
        }

        [HttpDelete("{id}")]
        public async Task<int> RemoveAuthor([FromHeader] string authorisation, int id) => await _authorsManager.RemoveAuthor(id);

        [HttpPut]
        public async Task<int> UpdateAutor([FromHeader] string authorisation, Author author) => await _authorsManager.UpdateAuthor(author);
    }
}
