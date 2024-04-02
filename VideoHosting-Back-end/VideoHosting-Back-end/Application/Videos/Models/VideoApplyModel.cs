namespace VideoHosting_Back_end.Application.Videos.Models;

public class VideoApplyModel
{
    public string UserId { get; set; }

    public string Name { get; set; }
    
    public string VideoPath { get; set; }

    public string PhotoPath { get; set; }
    
    public string Description { get; set; }
}
