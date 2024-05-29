using Dapper;
using LibraryAPI.Contracts;
using LibraryAPI.Models;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;

namespace LibraryAPI.Repositories
{
    public class AuthorsRepository :GenericRepository<Author>, IAuthorsRepository
    {
        private readonly IDbConnection _dbConnection;

        public AuthorsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }


    }
}
