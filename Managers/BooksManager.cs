using LibraryAPI.Contracts;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LibraryAPI.Managers
{
    public class BooksManager : IBooksManager
    {

        private readonly IBooksRepository _booksRepository;
        public BooksManager(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task AddBook(IFormFile file)
        {
            await _booksRepository.AddFile(file);
        }
        public async Task<IActionResult> GetAllBooks(string type)
        {
            return await _booksRepository.GetAllBooks(type);
        }

        public async Task<Book> GetBook(int id)
        {
            return await _booksRepository.Get(id);
        }

        public async Task<int> RemoveBook(int id)
        {
            return await _booksRepository.Remove(id);
        }

        public async Task<int> UpdateBook(Book book)
        {
            return await _booksRepository.Update(book);
        }


    }

}
