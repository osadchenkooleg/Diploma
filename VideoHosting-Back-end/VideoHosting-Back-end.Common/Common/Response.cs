namespace VideoHosting_Back_end.Common.Common;
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
