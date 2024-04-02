using MediatR;
using VideoHosting_Back_end.Application.Credentials.Commands;
using VideoHosting_Back_end.Application.Credentials.Models;
using VideoHosting_Back_end.Application.Users.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Common.Mappers;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;
using VideoHosting_Back_end.Database.UnitOfWork;

namespace VideoHosting_Back_end.Application.Credentials.Queries;

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
