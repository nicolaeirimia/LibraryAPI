using LibraryAPI.Models;

namespace LibraryAPI.Contracts
{
    public interface IMyApiClient
    {
        Task<HttpResponseMessage> GetBookAsync(string name);
    }
}
