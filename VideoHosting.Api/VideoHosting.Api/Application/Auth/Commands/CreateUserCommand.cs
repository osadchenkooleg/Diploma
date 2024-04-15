using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Identity;
using VideoHosting.Api.Application.Auth.Models;
using VideoHosting.Api.Common;
using VideoHosting.Common.Options;
using VideoHosting.Common.Responses;
using VideoHosting.Data.Entities;
using VideoHosting.Database.Abstraction;

namespace VideoHosting.Api.Application.Auth.Commands;

public class CreateUserCommand : IRequest<Response<UserGetModel>>
{
    public CreateUserCommand(UserApplyModel userApplyModel)
    {
        UserApplyModel = userApplyModel;
    }

    public UserApplyModel UserApplyModel { get; }

    public class Handler : BaseHandler<CreateUserCommand, UserGetModel>
    {
        private const string DefaultUserRole = "User";
        private const string AdminRole = "Admin";

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public Handler(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration, IUnitOfWork unit) : base(unit)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public override async Task<Response<UserGetModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.UserApplyModel;
            
            var user = new User()
            {
                Email = model.Email,
                UserName = model.Name + model.Surname,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Surname = model.Surname,
                Faculty = model.Faculty,
                Group = model.Group,
                Sex = model.Sex
            };

            var roles = new List<string> { DefaultUserRole };
            
            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if (identityResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, roles);
            }
            else
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, identityResult.Errors.FirstOrDefault()?.Description ?? "Something went wrong");
            }

            var result = new UserGetModel(user);

            return Success(result);
        }
    }
}
