using TechnicalTest.Shared;
using TechnicalTest.ViewModels;

namespace TechnicalTest.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginViewModel model);
        Task<RegisterResult> RegisterAsync(RegisterViewModel model, string token);
    }
}
