namespace VideoHosting.Api.Configuration;

public static class AuthorizationConfiguration
{
    public static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
    }
}