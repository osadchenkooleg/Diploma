using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoHosting.Api.Application.Credentials.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Users.Queries;

public class IsExistQuery : IRequest<Response<bool>>
{
    public LoginUserModel Model { get; }

    public IsExistQuery(LoginUserModel model)
    {
        Model = model;
    }

    public class Handler : BaseHandler<IsExistQuery, bool>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<bool>> Handle(IsExistQuery request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserManager.Users.FirstOrDefaultAsync(x => x.Email == request.Model.Email, cancellationToken);
           
            return Success(user != null);
        }
    }
}
