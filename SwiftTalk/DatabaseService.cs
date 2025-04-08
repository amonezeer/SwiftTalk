using Microsoft.EntityFrameworkCore;
using SwiftTalk.Data;
using SwiftTalk.Models;

namespace SwiftTalk
{
    public class DatabaseService
    {
        private readonly AuthDbContext _context;

        public DatabaseService(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string loginOrEmail, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => (u.Login == loginOrEmail || u.Email == loginOrEmail) && u.Password == password);
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameRegisteredAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Login == username);
        }

        public async Task RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}