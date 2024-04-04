using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting.Api.Common;
using VideoHosting.Common.Responses;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Credentials.Commands;

public class CreateAdminCommand : IRequest<Response<Unit>>
{
    public string Id { get; }

    public CreateAdminCommand(string id)
    {
        Id = id;
    }

    public class Handler : BaseHandler<CreateAdminCommand, Unit>
    {
        private const string AdminRole = "Admin";

        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            var isAdmin = await Unit.UserManager.IsInRoleAsync(user, AdminRole);
            if (!isAdmin)
            {
                await Unit.UserManager.AddToRoleAsync(user, AdminRole);
                await Unit.SaveAsync();
            }

            return Success(new Unit());
        }
    }
}
