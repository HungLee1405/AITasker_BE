using AITasker_Modular.Database;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AITasker_Modular.Modules.UserModule;

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public async Task<string> RegisterAsync(string email, string password, string fullName, string role)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        if (await _context.Users.AnyAsync(x => x.Email == normalizedEmail))
            return "Email already exists.";

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = normalizedEmail,
            PasswordHash = HashPassword(password),
            FullName = fullName.Trim(),
            Role = string.IsNullOrWhiteSpace(role) ? "Client" : role,
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _context.Wallets.Add(new Wallet
        {
            UserId = user.Id,
            Balance = 0m
        });

        await _context.SaveChangesAsync();

        return "Registered successfully.";
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email.Trim().ToLowerInvariant());
        if (user == null)
            return "Invalid email or password.";

        return VerifyPassword(password, user.PasswordHash) ? "Login successful." : "Invalid email or password.";
    }

    public async Task<decimal> DepositAsync(string userId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive.", nameof(amount));

        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        if (wallet == null)
            throw new InvalidOperationException($"Wallet not found for user ID: {userId}");

        wallet.Balance += amount;
        await _context.SaveChangesAsync();

        // Optionally, record a transaction log here

        return wallet.Balance;
    }

    public async Task<decimal> WithdrawAsync(string userId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive.", nameof(amount));

        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        if (wallet == null)
            throw new InvalidOperationException($"Wallet not found for user ID: {userId}");
        if (wallet.Balance < amount)
            throw new InvalidOperationException("Insufficient balance.");

        wallet.Balance -= amount;
        await _context.SaveChangesAsync();
        return wallet.Balance;
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);
        byte[] bytes = new byte[48];
        Buffer.BlockCopy(salt, 0, bytes, 0, 16);
        Buffer.BlockCopy(hash, 0, bytes, 16, 32);
        return Convert.ToBase64String(bytes);
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        byte[] bytes = Convert.FromBase64String(storedHash);
        byte[] salt = bytes.Take(16).ToArray();
        byte[] hash = bytes.Skip(16).ToArray();

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] computedHash = pbkdf2.GetBytes(hash.Length);

        return CryptographicOperations.FixedTimeEquals(computedHash, hash);
    }
}
