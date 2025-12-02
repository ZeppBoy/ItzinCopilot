using Itzin.Core.Entities;
using Itzin.Core.Interfaces;
using Itzin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itzin.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ItzinDbContext _context;

    public UserRepository(ItzinDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> CreateAsync(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailVerificationTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => 
            u.EmailVerificationToken == token && 
            u.EmailVerificationTokenExpiry > DateTime.UtcNow);
    }

    public async Task<User?> GetByPasswordResetTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => 
            u.PasswordResetToken == token && 
            u.PasswordResetTokenExpiry > DateTime.UtcNow);
    }
}
