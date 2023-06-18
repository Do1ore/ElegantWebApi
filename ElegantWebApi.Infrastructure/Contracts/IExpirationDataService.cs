namespace ElegantWebApi.Infrastructure.Contracts
{
    public interface IExpirationDataService
    {
        Task AddExpirationTimeAsync(string key, DateTime expirationTime);
        Task<DateTime> GetExpirationTimeAsync(string key);
        Task UpdateExpirationTimeAsync(string key, DateTime expirationTime);
        public Task<List<KeyValuePair<string, DateTime>>> GetAllAsync();
    }
}
