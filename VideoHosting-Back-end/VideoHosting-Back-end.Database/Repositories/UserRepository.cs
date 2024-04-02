using Microsoft.EntityFrameworkCore;
using VideoHosting_Back_end.Data.Entities;
using VideoHosting_Back_end.Database.Abstraction;
using VideoHosting_Back_end.Database.Context;

namespace VideoHosting_Back_end.Database.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserById(string id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUserBySubName(string str)
    {
        str = str.ToLower();
        return await _context.Users
            .Where(x => x.Name.ToLower().Contains(str) || x.Surname.ToLower().Contains(str) || (x.Name + x.Surname)
                .ToLower()
                .Contains(str))
            .ToListAsync();
    }
}
