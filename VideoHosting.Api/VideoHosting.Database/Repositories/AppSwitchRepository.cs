using VideoHosting.Database.Abstraction;
using VideoHosting.Database.Context;

namespace VideoHosting.Database.Repositories;
public class AppSwitchRepository : IAppSwitchRepository
{
    private readonly ApplicationContext _context;

    public AppSwitchRepository(ApplicationContext context)
    {
        _context = context;
    }

    public string? GetValue(string key)
    {
        var appSwitch = _context.AppSwitches.FirstOrDefault(x => x.Key == key);
        return appSwitch?.Value;
    }
}
