namespace TechnicalTest.Interfaces
{
    public interface IEmailService
    {
        Task SendLockoutEmailAsync(string email, string fullName);
    }
}
