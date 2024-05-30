using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Contracts
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task<IActionResult> GetAllBooks(string type);
    }
}
