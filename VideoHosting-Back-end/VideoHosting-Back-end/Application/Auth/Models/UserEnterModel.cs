using System.ComponentModel.DataAnnotations;

namespace VideoHosting_Back_end.Application.Auth.Models;

public class UserEnterModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
