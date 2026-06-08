namespace AITasker_Modular.Modules.UserModule;

public interface IUserService
{
    Task<string> RegisterAsync(string email, string password, string fullName, string role);
    Task<string> LoginAsync(string email, string password);
    Task<decimal> DepositAsync(string userId, decimal amount); // Changed Guid to string
    Task<decimal> WithdrawAsync(string userId, decimal amount); // Changed Guid to string
}