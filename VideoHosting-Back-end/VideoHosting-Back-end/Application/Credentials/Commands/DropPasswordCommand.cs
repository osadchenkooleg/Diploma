using Hellang.Middleware.ProblemDetails;
using MediatR;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;
using VideoHosting_Back_end.Database.UnitOfWork;

namespace VideoHosting_Back_end.Application.Credentials.Commands;

public class DropPasswordCommand : IRequest<Response<int>>
{
    public string Email { get; }

    public DropPasswordCommand(string email)
    {
        Email = email;
    }

    public class Handler : BaseHandler<DropPasswordCommand, int>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<int>> Handle(DropPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await Unit.UserManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            var password = new Random().Next(1000, 1000000);
            user.TempPassword = password;

            await Unit.SaveAsync();
            return Success(password);
        }
    }
}
