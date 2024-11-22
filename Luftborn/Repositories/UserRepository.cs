namespace Luftborn.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();
    public async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
    public async Task AddAsync(User user) => await _context.Users.AddAsync(user);
    public async Task UpdateAsync(User user) => _context.Users.Update(user);
    public async Task DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
    }
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

}
