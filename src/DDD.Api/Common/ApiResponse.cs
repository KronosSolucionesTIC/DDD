namespace DDD.Api.Common;

public class ApiResponse
{
    public bool Success { get; protected set; }
    public string? Message { get; protected set; }

    public static ApiResponse Ok(string? message = null)
        => new ApiResponse
        {
            Success = true,
            Message = message
        };

    public static ApiResponse Fail(string message)
        => new ApiResponse
        {
            Success = false,
            Message = message
        };
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; private set; }

    public static ApiResponse<T> Ok(T data, string? message = null)
        => new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };

    public static ApiResponse<T> Fail(string message)
        => new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default
        };
}
