using Hellang.Middleware.ProblemDetails;
using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

namespace VideoHosting.Api.Configuration;

public static class ProblemDetailsConfiguration
{
    public static void ConfigureProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(ConfigureProblemDetailsOptions);
    }

    private static void ConfigureProblemDetailsOptions(ProblemDetailsOptions o)
    {
        o.ValidationProblemStatusCode = StatusCodes.Status400BadRequest;
    }
}
