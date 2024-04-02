using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace VideoHosting_Back_end.Common.Configuration;
public class AuthConfigurationOptions : AppOptions
{
    private const string SectionName = "Authentication:Jwt";

    public AuthConfigurationOptions(IConfiguration configuration) : base(configuration, SectionName)
    {
    }
    
    public string Issuer => _configuration.GetSection("Issuer").Value;

    public string Audience => _configuration.GetSection("Audience").Value;

    public string SecretKey => _configuration.GetSection("Key").Value;

    public double LifeTime => double.Parse(_configuration.GetSection("Expires").Value);
}

