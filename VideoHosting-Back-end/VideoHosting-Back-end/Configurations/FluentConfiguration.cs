using FluentValidation.AspNetCore;
using FluentValidation;

namespace VideoHosting_Back_end.Configurations;

public static class FluentConfiguration
{
    public static void ConfigureFluentValidation<T>(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<T>();
    }
}
