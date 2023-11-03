using TodoXpress.Domain;

namespace TodoXpress.Api.Data;

public readonly record struct ErrorResponse
{
    public string ErrorType { get; init; }

    public string Description { get; init; }

    public bool IsSuccess => false;

    public bool IsError => !IsSuccess;

    public static ErrorResponse Create(IError error)
    {
        return new ErrorResponse()
        {
            ErrorType = error.Type.Name,
            Description = error.Description
        };
    }
}
