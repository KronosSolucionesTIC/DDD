using DDD.Application.Common;

namespace DDD.Api.Common;

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
}
