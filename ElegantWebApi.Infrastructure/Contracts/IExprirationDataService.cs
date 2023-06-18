namespace ElegantWebApi.Infrastructure.Contracts
{
    public interface IExprirationDataService
    {
        public Task AddExpirationTimeAsync(string key, DateTime expirationTime);
        public Task<DateTime> GetExprirationTimeAsync(string key);
        public Task UpdateExparationTimeAsync(string key, DateTime expirationTime);
        public Task<List<KeyValuePair<string, DateTime>>> GetAllAsync();
        public Task RemoveAsync(string key);
    }
}
