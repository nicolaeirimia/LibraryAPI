namespace LibraryAPI.Contracts
{
    public interface IGenericRepository<T> where T : class 
    {
        Task Add(T t);
        Task<int> Remove(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<int> Update(T t);
        public Task AddFile(IFormFile file);
    }
}
