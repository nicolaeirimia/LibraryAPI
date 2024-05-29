using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Contracts
{
    public interface IAuthorsManager
    {
        public Task AddAuthor(IFormFile file);
        Task<int> RemoveAuthor(int id);
        Task<IActionResult> GetAllAuthors();
        Task<Author> GetAuthor(int id);
        Task<int> UpdateAuthor(Author author);
    }
}
