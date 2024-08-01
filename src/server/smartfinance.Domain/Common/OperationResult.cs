namespace smartfinance.Domain.Common
{
    public class OperationResult<T>
    {
        public T Model { get; set; }

        public bool Success { get; set; } = false;

        public IEnumerable<Error>? Errors { get; set; }

        public string Message { get; set; }

        public static OperationResult<T> Succeeded() => new()
        {
            Success = true,
            Model = default,
        };

        public static OperationResult<T> Succeeded(T payLoad) => new()
        {
            Success = true,
            Model = payLoad,
        };

        public static OperationResult<T> Failed(IEnumerable<Error> errors, string? message = null)
        => new()
        {
            Message = message,
            Errors = errors
        };

        public static OperationResult<T> Failed(string message) => new()
        {
            Message = message,
        };
    }
    public class Error(string code, string description)
    {
        public string Code { get; set; } = code;
        public string Description { get; set; } = description;
    }
}
