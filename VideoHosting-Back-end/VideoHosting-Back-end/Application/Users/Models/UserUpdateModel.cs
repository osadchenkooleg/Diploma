namespace VideoHosting_Back_end.Application.Users.Models;

public class UserUpdateModel
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Faculty { get; set; }

    public string? Group { get; set; }
    
    public bool? Sex { get; set; }
}
