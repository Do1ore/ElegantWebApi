namespace ElegantWebApi.Infrastructure.Contracts
{
    public interface IExpirationDataService
    {
        public Task AddExpirationTimeAsync(string key, DateTime expirationTime);
        public Task<DateTime> GetExpirationTimeAsync(string key);
        public Task UpdateExpirationTimeAsync(string key, DateTime expirationTime);
        public Task<List<KeyValuePair<string, DateTime>>> GetAllAsync();
        public Task RemoveAsync(string id);
    }
}