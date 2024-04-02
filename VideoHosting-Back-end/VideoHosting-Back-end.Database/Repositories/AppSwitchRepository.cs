using VideoHosting_Back_end.Database.Abstraction;
using VideoHosting_Back_end.Database.Context;

namespace VideoHosting_Back_end.Database.Repositories;
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
