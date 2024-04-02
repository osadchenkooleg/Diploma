using Microsoft.Extensions.Configuration;

namespace VideoHosting_Back_end.Common.Configuration;
public abstract class AppOptions
{
    protected readonly IConfiguration _configuration;

    protected AppOptions(IConfiguration configuration, string sectionName)
    {
        _configuration = configuration.GetSection(sectionName);
    }
}
