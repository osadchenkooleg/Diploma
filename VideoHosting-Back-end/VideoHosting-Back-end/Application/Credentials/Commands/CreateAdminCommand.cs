using System.Runtime.CompilerServices;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Application.Commentaries.Commands;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;
using VideoHosting_Back_end.Database.UnitOfWork;

namespace VideoHosting_Back_end.Application.Credentials.Commands;

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
