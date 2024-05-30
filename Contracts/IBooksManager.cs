using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Contracts
{
    public interface IBooksManager
    {
        public Task AddBook(IFormFile file);
        Task<int> RemoveBook(int id);
        Task<IActionResult> GetAllBooks(string type);
        Task<Book> GetBook(int id);
        Task<int> UpdateBook(Book book);


    }
}
