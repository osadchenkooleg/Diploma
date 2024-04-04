namespace VideoHosting.Common.Responses;
public class Response<TOutput>
{
    public Response(TOutput sideData, bool isSuccess = true)
    {
        Result = sideData;
        IsSuccess = isSuccess;
    }
    public TOutput Result { get; }

    public bool IsSuccess { get; }
}
