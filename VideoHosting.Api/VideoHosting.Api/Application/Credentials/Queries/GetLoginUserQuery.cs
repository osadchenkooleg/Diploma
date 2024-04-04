using MediatR;
using VideoHosting.Api.Application.Credentials.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Credentials.Queries;

public class GetLoginUserQuery  : IRequest<Response<UserLoginGetModel>>
{
    public string UserId { get; }

    public GetLoginUserQuery(string userId)
    {
        UserId = userId;
    }
    public class Handler : BaseHandler<GetLoginUserQuery, UserLoginGetModel>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<UserLoginGetModel>> Handle(GetLoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserManager.FindByIdAsync(request.UserId);
            return Success(user.MapToUserLoginGetModel());
        }
    }
}
