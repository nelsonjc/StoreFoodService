using System.Text.Json;

namespace ShopFood.Domain.DTOs.Results
{
    /// <summary>
    /// Http Error Generic Response 
    /// </summary>
    public class HttpErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Description { get; set; }
        public string? ExceptionMessage { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
