using MediatR;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Common;

public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, Response<TResponse>>
    where TRequest : IRequest<Response<TResponse>>
{
    protected BaseHandler(IUnitOfWork unit)
    {
        Unit = unit;
    }

    public IUnitOfWork Unit { get; set; }

    public abstract Task<Response<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

    protected Response<TResponse> Success(TResponse result)
    {
        return new Response<TResponse>(result);
    }
}
