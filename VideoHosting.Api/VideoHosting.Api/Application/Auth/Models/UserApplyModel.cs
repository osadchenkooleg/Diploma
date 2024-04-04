using System.ComponentModel.DataAnnotations;

namespace VideoHosting.Api.Application.Auth.Models;

public class UserApplyModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string PasswordConfirm { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string Faculty { get; set; }
        
    [Required]
    public string Group { get; set; }

    [Required]
    public bool Sex { get; set; }
}
