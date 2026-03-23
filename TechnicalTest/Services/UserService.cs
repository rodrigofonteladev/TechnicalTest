using TechnicalTest.Data;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.Interfaces;
using TechnicalTest.ViewModels;

namespace TechnicalTest.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfileViewModel> GetProfileAsync(int userId)
        {
            var user = await _context.ApplicationUsers
                .Include(u => u.Role)
                .Include(u => u.Ministry)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            return new UserProfileViewModel
            {
                FullName = user.FullName,
                FirstName = user.FirstName,
                FirstLastName = user.FirstLastName,
                SecondLastName = user.SecondLastName,
                DocumentType = user.DocumentType,
                DocumentNumber = user.DocumentNumber,
                BirthDate = user.BirthDate,
                Nationality = user.Nationality,
                Gender = user.Gender,
                Email = user.Email,
                SecondaryEmail = user.SecondaryEmail,
                PhoneNumber = user.PhoneNumber,
                FormattedPhoneNumber = user.FormattedPhoneNumber,
                SecondaryPhoneNumber = user.SecondaryPhoneNumber,
                FormattedSecondaryPhoneNumber = user.SecondaryFormattedPhoneNumber,
                TypeSecondaryPhoneNumber = user.TypeSecondaryPhoneNumber,
                TypeContract = user.TypeContract,
                HiringDate = user.HiringDate,
                ProfilePicture = user.ProfilePicture,
                IsActive = user.IsActive,
                RoleName = user.Role.Name,
                MinistryName = user.Ministry.Name
            };
        }
    }
}
