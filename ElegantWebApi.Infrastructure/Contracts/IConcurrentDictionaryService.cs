namespace ElegantWebApi.Infrastructure.Contracts
{
    public interface IConcurrentDictionaryService
    {
        Task Append(string key, object value);
        Task Create(string key, List<object> list);
        Task<List<object>> Delete(string id);
        Task<List<object>> Get(string key);
    }
}