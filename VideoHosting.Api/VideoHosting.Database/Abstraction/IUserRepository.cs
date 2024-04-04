using VideoHosting.Data.Entities;

namespace VideoHosting.Database.Abstraction;
public interface IUserRepository
{       
    Task<User?> GetUserById(string id);

    Task<IEnumerable<User>> GetUsers();

    Task<IEnumerable<User>> GetUserBySubName(string str);
}
