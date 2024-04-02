using Microsoft.Extensions.Configuration;

namespace VideoHosting_Back_end.Common.Configuration;
public class ConnectionStringOptions : AppOptions
{
    private const string SectionName = "ConnectionStrings";

    public ConnectionStringOptions(IConfiguration configuration) : base(configuration, SectionName)
    {
    }

    public string DefaultConnection  => _configuration.GetSection("DefaultConnection").Value;
}

