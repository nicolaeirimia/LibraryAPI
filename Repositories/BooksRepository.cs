using CsvHelper;
using CsvHelper.Configuration;
using LibraryAPI.Contracts;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace LibraryAPI.Repositories
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        private readonly IDbConnection _dbConnection;

        public BooksRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IActionResult> GetAllBooks(string type)
        {
            IEnumerable<Book> books = await GetAll();

            try
            {
                var stream = new MemoryStream();

                if (type.Equals("json", StringComparison.OrdinalIgnoreCase))
                {
                    return await ExportToJsonAsync(books, stream);
                }
                else if (type.Equals("xml", StringComparison.OrdinalIgnoreCase))
                {
                    return ExportToXml(books, stream);
                }
                else if (type.Equals("csv", StringComparison.OrdinalIgnoreCase))
                {
                    return await ExportToCsvAsync(books, stream);
                }
                else
                {
                    throw new ArgumentException("Invalid type parameter");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}", ex);
            }
        }


        private async Task<FileStreamResult> ExportToJsonAsync(IEnumerable<Book> books, MemoryStream stream)
        {
            string json = JsonConvert.SerializeObject(books);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await stream.WriteAsync(bytes, 0, bytes.Length);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/json")
            {
                FileDownloadName = "all_books.json"
            };

        }

        private FileStreamResult ExportToXml(IEnumerable<Book> books, MemoryStream stream)
        {
            var serializer = new XmlSerializer(typeof(List<Book>));
            serializer.Serialize(stream, books.ToList());
            stream.Position = 0;
            return new FileStreamResult(stream, "application/xml")
            {
                FileDownloadName = "all_books.xml"
            };
        }

        private async Task<FileStreamResult> ExportToCsvAsync(IEnumerable<Book> books, MemoryStream stream)
        {
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                await csv.WriteRecordsAsync(books);
            }
            stream.Position = 0;
            return new FileStreamResult(stream, "text/csv")
            {
                FileDownloadName = "all_books.csv"
            };
        }
    }
}
