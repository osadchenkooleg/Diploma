using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;
using VideoHosting_Back_end.Database.UnitOfWork;

namespace VideoHosting_Back_end.Application.Credentials.Commands;

public class RemoveAdminCommand : IRequest<Response<Unit>>
{
    public string Id { get; }

    public RemoveAdminCommand(string id)
    {
        Id = id;
    }

    public class Handler : BaseHandler<RemoveAdminCommand, Unit>
    {
        private const string AdminRole = "Admin";
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(RemoveAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            if (await Unit.UserManager.IsInRoleAsync(user, AdminRole))
            {
                await Unit.UserManager.RemoveFromRoleAsync(user, AdminRole);
                await Unit.SaveAsync();
            }

            return Success(new Unit());
        }
    }
}
