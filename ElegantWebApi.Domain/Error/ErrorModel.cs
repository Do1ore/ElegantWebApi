namespace ElegantWebApi.Domain.Error
{
    [Serializable]
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
