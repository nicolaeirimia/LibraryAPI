using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Contracts
{
    public interface IBooksManager
    {
        public Task AddBook(IFormFile file);
        Task<int> RemoveBook(int id);
        Task<IActionResult> GetAllBooks();
        Task<Book> GetBook(int id);
        Task<int> UpdateBook(Book book);


    }
}
