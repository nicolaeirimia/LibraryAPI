using LibraryAPI.Contracts;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LibraryAPI.Managers
{
    public class AuthorsManager : IAuthorsManager
    {


        private readonly IAuthorsRepository _authorsRepository;
        public AuthorsManager(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }
        public Task AddAuthor(IFormFile file)
        {
            try
            {
                return _authorsRepository.AddFile(file);
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public async Task<IActionResult> GetAllAuthors()
        {
            IEnumerable<Author> books = await _authorsRepository.GetAll();

            string json = JsonConvert.SerializeObject(books);

            byte[] bytes = Encoding.UTF8.GetBytes(json);

            try
            {
                MemoryStream stream = new MemoryStream(bytes);
                return new FileStreamResult(stream, "application/json")
                {
                    FileDownloadName = "all_authors.json"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }

        public async Task<Author> GetAuthor(int id)
        {
            return await _authorsRepository.Get(id);
        }

        public async Task<int> RemoveAuthor(int id)
        {
            return await _authorsRepository.Remove(id);
        }

        public async Task<int> UpdateAuthor(Author author)
        {
           return await _authorsRepository.Update(author);
        }
    }
}
