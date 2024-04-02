using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoHosting_Back_end.Application.Credentials.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Users.Queries;

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
