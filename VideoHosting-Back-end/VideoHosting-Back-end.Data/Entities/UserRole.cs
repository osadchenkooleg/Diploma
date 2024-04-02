using Microsoft.AspNetCore.Identity;

namespace VideoHosting_Back_end.Data.Entities;
public class UserRole : IdentityRole
{
    public string Description { get; set; }
}
