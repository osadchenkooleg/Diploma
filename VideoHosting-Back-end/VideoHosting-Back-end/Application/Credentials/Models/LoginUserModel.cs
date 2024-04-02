using System.ComponentModel.DataAnnotations;

namespace VideoHosting_Back_end.Application.Credentials.Models;

public class LoginUserModel
{
    [Required]
    public string Email { get; set; }
}
