using DDD.Api.Common;
using DDD.Application.Common;

public static class ApiResponseMapper
{
    public static ApiResponse<T> FromResult<T>(
        Result<T> result,
        string? successMessage = null
    )
    {
        return result.IsSuccess
            ? ApiResponse<T>.Ok(result.Value!, successMessage)
            : ApiResponse<T>.Fail(result.Error!);
    }

    public static ApiResponse Success(string message)
        => ApiResponse.Ok(message);

    public static ApiResponse Fail(string message)
        => ApiResponse.Fail(message);
}