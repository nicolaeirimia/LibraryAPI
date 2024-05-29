using Dapper;
using LibraryAPI.Contracts;
using LibraryAPI.Managers;
using LibraryAPI.Models;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;


namespace LibraryAPI.Repositories
{
    public class BooksRepository : GenericRepository<Book>,  IBooksRepository
    {
        private readonly IDbConnection _dbConnection;

        public BooksRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }


    }
}
