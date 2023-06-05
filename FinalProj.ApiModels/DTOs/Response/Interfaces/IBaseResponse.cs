namespace FinalProj.ApiModels.Response.Interfaces
{
    public interface IBaseResponse<T>
    {
        public T Data { get; set; }
        public Guid? requestId { get; set; }
        public string? userId { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
