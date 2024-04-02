using Microsoft.Extensions.Configuration;

namespace VideoHosting_Back_end.Common.Configuration;
public class AppSwitchOptions : AppOptions
{
    private const string SectionName = "Settings";

    public AppSwitchOptions(IConfiguration configuration) : base(configuration, SectionName)
    {
    }
    
    public string UserPhotoKey => _configuration.GetSection("UserPhoto").Value;

    public string VideoPhotoKey => _configuration.GetSection("VideoPhoto").Value;

    public string VideoKey => _configuration.GetSection("Video").Value;
}
