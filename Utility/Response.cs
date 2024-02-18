namespace JobPortal.Utility;

public static class ResponseFactory
{
    public static Response Ok() => new(true, default);
    public static Response<T> Ok<T>(T? data = default) => new(true, data, default);

    public static Response Fail() => new(false, default);
    public static Response Fail(Error error) => new(false, new[] {error});
    public static Response Fail(IEnumerable<Error> errors) => new(false, errors);
    public static Response<T> Fail<T>() => new(false, default, default);
    public static Response<T> Fail<T>(Error error, T? data = default) => Fail(new[] {error}, data);
    public static Response<T> Fail<T>(IEnumerable<Error> errors, T? data = default) => new(false, data, errors);

    public static Response FailFromException(Exception ex)
    {
        var err = new Error {Code = ex.Message, Description = ex.InnerException?.Message ?? ""};
        return Fail(err);
    }

    public static Response ResponseFromErrors(IEnumerable<Error> errorList) => errorList.Any() ? Fail(errorList) : Ok();

    public static Response<T> FailFromException<T>(Exception ex)
    {
        var err = new Error {Code = ex.Message, Description = ex.InnerException?.Message ?? ""};
        return Fail<T>(err);
    }
}

public class Response
{
    public Response(bool succeeded, IEnumerable<Error>? errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public bool Succeeded { get; }
    public IEnumerable<Error>? Errors { get; }

   
}

public class Response<T> : Response
{
    public Response(bool succeeded, T? data, IEnumerable<Error>? errors)
        : base(succeeded, errors)
    {
        Data = data;
    }

    public T? Data { get; }
}