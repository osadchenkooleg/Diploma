using Microsoft.Extensions.Configuration;

namespace VideoHosting.Common.Options;

public abstract class AppOptions
{
    protected readonly IConfiguration _configuration;

    protected AppOptions(IConfiguration configuration, string sectionName)
    {
        _configuration = configuration.GetSection(sectionName);
    }
}
