﻿using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VideoHosting_Back_end.Application.Auth.Models;
using VideoHosting_Back_end.Common.Common;
using VideoHosting_Back_end.Common.Configuration;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;

namespace VideoHosting_Back_end.Application.Auth.Commands;

public class LoginCommand : IRequest<Response<TokenGetModel>>
{
    public LoginCommand(UserEnterModel loginModel)
    {
        LoginModel = loginModel;
    }

    public UserEnterModel LoginModel { get; }

    public class Handler : BaseHandler<LoginCommand, TokenGetModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IConfiguration _configuration;

        public Handler(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager, IConfiguration configuration, IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public override async Task<Response<TokenGetModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var model = request.LoginModel;
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            var isValid = user is not null && isValidPassword;
            if (!isValid)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Invalid login or password");
            }
            
            var roles = await _userManager.GetRolesAsync(user);

            var result = new TokenGetModel()
            {
                Token = CreateToken(user, roles),
            };

            return Success(result);
        }

        private string CreateToken(User User, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var authOptions = new AuthConfigurationOptions(_configuration);
            
            var key = authOptions.SecretKey;
            var claims = CreateClaims(User, roles);

            var tokenDescriptor = CreateSecurityTokenDescriptor(claims, authOptions, key);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static IEnumerable<Claim> CreateClaims(User User, IEnumerable<string> roles)
        {
            return roles
                .Select(role => new Claim(ClaimTypes.Role, role))
                .Append(new Claim(ClaimTypes.Name, User.UserName))
                .Append(new Claim(ClaimTypes.Email, User.Email))
                .Append(new Claim(ClaimTypes.NameIdentifier, User.Id));
        }

        private static SecurityTokenDescriptor CreateSecurityTokenDescriptor(
            IEnumerable<Claim> claims,
            AuthConfigurationOptions authOptions,
            string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            var securityKey = new SymmetricSecurityKey(bytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = authOptions.Issuer,
                Audience = authOptions.Audience,
                Expires = DateTime.UtcNow.AddDays(authOptions.LifeTime),
                SigningCredentials = credentials,
            };
        }
    }
}
