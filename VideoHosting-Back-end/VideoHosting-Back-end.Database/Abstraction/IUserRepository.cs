using VideoHosting_Back_end.Data.Entities;

namespace VideoHosting_Back_end.Database.Abstraction;
public interface IUserRepository
{       
    Task<User?> GetUserById(string id);

    Task<IEnumerable<User>> GetUsers();

    Task<IEnumerable<User>> GetUserBySubName(string str);
}
