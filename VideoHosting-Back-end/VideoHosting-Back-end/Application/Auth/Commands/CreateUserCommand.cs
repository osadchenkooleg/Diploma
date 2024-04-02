using MediatR;
using VideoHosting_Back_end.Application.Auth.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Common.Mappers;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Auth.Commands;

public class CreateUserCommand : IRequest<Response<Unit>>
{
    public UserRegistrationModel UserRegistrationModel { get; }

    public CreateUserCommand(UserRegistrationModel userRegistrationModel)
    {
        UserRegistrationModel = userRegistrationModel;
    }
    
    public class Handler : BaseHandler<CreateUserCommand, Unit>
    {
        public Handler(IUnitOfWork unit) : base(unit)
        {
        }

        public override async Task<Response<Unit>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.UserRegistrationModel.MapToUser();

            await Unit.UserManager.CreateAsync(user, request.UserRegistrationModel.Password);
            await Unit.UserManager.UpdateAsync(user);
            await Unit.UserManager.AddToRoleAsync(user, "User");
            await Unit.SaveAsync();

            return Success(new Unit());
        }

        
    }
}
