using LibraryAPI.Contracts;
using System.Data;
using static Dapper.SqlMapper;
using System.Text;
using Dapper;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using LibraryAPI.Classes;
using System.Formats.Asn1;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using CsvHelper;
using CsvHelper.Configuration;
using System.Text.RegularExpressions;

namespace LibraryAPI.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly IDbConnection _dbConnection;
        protected static string TableName => TableNameMapper.GetTableName(typeof(T));
        protected GenericRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public virtual Task Add(T t)
        {
            try
            {
                var insertQuery = GenerateInsertQuery();
                return _dbConnection.ExecuteAsync(insertQuery, t);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<T> Get(int id)
        {
            try
            {
                var query = $"SELECT * FROM {TableName} WHERE {typeof(T).ToString().ToLower().Substring(18)}_id = @Id";
                return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id }) ?? throw new Exception("Doest't Exist");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                var query = $"SELECT * FROM {TableName}";
                return await _dbConnection.QueryAsync<T>(query);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public virtual async Task<int> Remove(int id)
        {
            try
            {
                var deleteQuery = $"DELETE FROM {TableName} WHERE {typeof(T).ToString().ToLower().Substring(18)}_id = @Id";
                return await _dbConnection.ExecuteAsync(deleteQuery, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public virtual async Task<int> Update(T t)
        {
            try
            {
                var updateQuery = GenerateUpdateQuery();
                return await _dbConnection.ExecuteAsync(updateQuery, t);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task AddFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File didn't load!");
            }

            try
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                switch (fileExtension)
                {
                    case ".json":
                        await ProcessJsonFile(file);
                        break;

                    case ".csv":
                        await ProcessCsvFile(file);
                        break;

                    case ".xml":
                        await ProcessXmlFile(file);
                        break;

                    default:
                        throw new ArgumentException("Unsupported file format. Please upload a JSON, CSV, or XML file.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while handling the file: {ex.Message}");
            }
        }

        private async Task ProcessJsonFile(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var jsonContent = await reader.ReadToEndAsync();
                var entities = JsonConvert.DeserializeObject<List<T>>(jsonContent);

                if (entities == null || entities.Count == 0)
                {
                    throw new ArgumentException($"JSON file doesn't contain valid entries for {typeof(T)} type.");
                }

                foreach (var entity in entities)
                {
                    var insertQuery = GenerateInsertQuery();
                    await _dbConnection.ExecuteAsync(insertQuery, entity);
                }
            }
        }

        private async Task ProcessCsvFile(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var entities = csv.GetRecords<T>().ToList();

                if (entities == null || entities.Count() == 0)
                {
                    throw new ArgumentException($"CSV file doesn't contain valid entries for {typeof(T)} type.");
                }

                foreach (var entity in entities)
                {
                    var insertQuery = GenerateInsertQuery();
                    await _dbConnection.ExecuteAsync(insertQuery, entity);
                }
            }
        }

        private async Task ProcessXmlFile(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var xmlContent = await reader.ReadToEndAsync();
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "books";
                var serializer = new XmlSerializer(typeof(List<T>), xRoot);
                using (var stringReader = new StringReader(xmlContent))
                {
                    var entities = (List<T>)serializer.Deserialize(stringReader);

                    if (entities == null || entities.Count() == 0)
                    {
                        throw new ArgumentException($"XML file doesn't contain valid entries for {typeof(T)} type.");
                    }

                    foreach (var entity in entities)
                    {
                        var insertQuery = GenerateInsertQuery();
                        await _dbConnection.ExecuteAsync(insertQuery, entity);
                    }
                }
            }
        }

        protected string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {TableName} ");
            insertQuery.Append("(");
            var properties = typeof(T).GetProperties().Select(p => p.Name);
            properties = properties.Where(p => !p.Equals($"{typeof(T)}_Id", StringComparison.OrdinalIgnoreCase));
            insertQuery.Append(string.Join(", ", properties));
            insertQuery.Append(") VALUES (");
            insertQuery.Append(string.Join(", ", properties.Select(p => "@" + p)));
            insertQuery.Append(")");

            return insertQuery.ToString();
        }

        protected string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {TableName} SET ");
            var properties = typeof(T).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));

            var idProperty = typeof(T).GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)));

            if (idProperty == null)
            {
                throw new InvalidOperationException($"Entity does not have an Id property. {typeof(T)}_Id"); ///book_Id   LibraryAPI.bookI
            }

            foreach (var property in properties)
            {
                updateQuery.Append($"{property.Name} = @{property.Name}, ");
            }
            updateQuery.Length -= 2;

            updateQuery.Append($" WHERE {idProperty.Name} = @{idProperty.Name}");

            return updateQuery.ToString();
        }

      
    }
}
