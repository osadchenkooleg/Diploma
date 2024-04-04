using Microsoft.Extensions.Configuration;

namespace VideoHosting.Common.Options;

public class AuthConfigurationOptions : AppOptions
{
    private const string SectionName = "Authentication:Jwt";

    public AuthConfigurationOptions(IConfiguration configuration) : base(configuration, SectionName)
    {
    }

    public string Authority => _configuration.GetSection("Authority").Value;

    public string Issuer => _configuration.GetSection("Issuer").Value;

    public string Audience => _configuration.GetSection("Audience").Value;

    public string SecretKey => _configuration.GetSection("Key").Value;

    public double LifeTime => double.Parse(_configuration.GetSection("Expires").Value);

    public bool RequireHttpsMetadata => Issuer?.StartsWith("https") == true;
}
