namespace VideoHosting.Database.Abstraction;
public interface IAppSwitchRepository
{
    string? GetValue(string key);
}
