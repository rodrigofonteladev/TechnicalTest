using TechnicalTest.ViewModels;

namespace TechnicalTest.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileViewModel> GetProfileAsync(int userId);
    }
}
