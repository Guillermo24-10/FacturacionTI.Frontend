namespace FacturacionTI.UI.Models
{
    public class BaseResponse<T> where T : class
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public T Data { get; set; } = null!;
    }
}
