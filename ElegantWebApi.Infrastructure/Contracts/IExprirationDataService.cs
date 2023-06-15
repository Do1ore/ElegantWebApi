namespace ElegantWebApi.Infrastructure.Contracts
{
    public interface IExprirationDataService
    {
        Task AddExpirationTimeAsync(string key, DateTime expirationTime);
        Task<DateTime> GetExprirationTimeAsync(string key);
        Task UpdateExparationTimeAsync(string key, DateTime expirationTime);


    }
}
