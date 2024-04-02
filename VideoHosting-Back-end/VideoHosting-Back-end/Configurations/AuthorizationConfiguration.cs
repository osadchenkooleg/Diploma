namespace VideoHosting_Back_end.Configurations;

public static class AuthorizationConfiguration
{
    public static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
    }
}
