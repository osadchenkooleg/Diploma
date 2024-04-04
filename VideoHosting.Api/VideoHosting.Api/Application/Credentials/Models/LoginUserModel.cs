using System.ComponentModel.DataAnnotations;

namespace VideoHosting.Api.Application.Credentials.Models;

public class LoginUserModel
{
    [Required]
    public string Email { get; set; }
}
