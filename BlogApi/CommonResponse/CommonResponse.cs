namespace CommonResponse.Models
{
    public class CommonResponse
    {
        public int statusCode { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }
    }
}