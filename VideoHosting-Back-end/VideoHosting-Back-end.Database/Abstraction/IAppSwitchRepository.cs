namespace VideoHosting_Back_end.Database.Abstraction;
public interface IAppSwitchRepository
{
    string? GetValue(string key);
}
