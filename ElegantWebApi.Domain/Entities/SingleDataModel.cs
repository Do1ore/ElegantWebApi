namespace ElegantWebApi.Domain.Entities
{
    public class SingleDataModel
    {
        public Guid Id { get; set; }
        public object Value { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
