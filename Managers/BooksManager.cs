using LibraryAPI.Contracts;
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
        public async Task<IActionResult> GetAllBooks()
        {
            IEnumerable<Book> books = await _booksRepository.GetAll();

            string json = JsonConvert.SerializeObject(books);

            byte[] bytes = Encoding.UTF8.GetBytes(json);

            try
            {
                MemoryStream stream = new MemoryStream(bytes);
                return new FileStreamResult(stream, "application/json")
                {
                    FileDownloadName = "all_books.json"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }

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
