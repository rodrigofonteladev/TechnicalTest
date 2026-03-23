using TechnicalTest.Data;
using TechnicalTest.Interfaces;
using TechnicalTest.Models;
using TechnicalTest.Shared;
using TechnicalTest.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public AuthService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<AuthResult> LoginAsync(LoginViewModel model)
        {
            var user = await _context.ApplicationUsers.
                Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.NormalizedUserName == model.UserName.Trim().ToUpper());
            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Nombre de usuario o contraseña incorrectos"
                };
            }
            if (!user.IsActive)
            {
                return new AuthResult 
                { 
                    Success = false,
                    Message = "Su cuenta aún no ha sido activada." 
                };
            }

            if (user.LockoutEnd.HasValue)
            {
                if (user.LockoutEnd > DateTime.UtcNow)
                {
                    return new AuthResult
                    {
                        Success = false,
                        IsLocked = true,
                        ApplicationUser = user
                    };
                }
                else
                {
                    user.FailedLoginAttempts = 0;
                    user.LockoutEnd = null;
                    await _context.SaveChangesAsync();
                }
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);

            if (!isValid)
            {
                user.FailedLoginAttempts++;
                bool racheLimit = false;

                if (user.FailedLoginAttempts >= 5)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(15);
                    racheLimit = true;
                    _emailService.SendLockoutEmailAsync(user.Email, user.FullName);
                }

                await _context.SaveChangesAsync();
                return new AuthResult
                { 
                    Success = false,
                    IsLocked = racheLimit,
                    Message = "Nombre de usuario o contraseña incorrectos",
                    ApplicationUser = user
                };
            }

            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            await _context.SaveChangesAsync();

            return new AuthResult
            {
                Success = true,
                ApplicationUser = user
            };
        }

        public async Task<RegisterResult> RegisterAsync(RegisterViewModel model, string token)
        {
            bool isUserExists = await _context.ApplicationUsers.AnyAsync(u => 
                u.Email == model.Email ||
                u.NormalizedUserName == model.UserName.Trim().ToUpper() || 
                u.DocumentNumber == model.DocumentNumber);
            
            if (isUserExists)
            {
                return new RegisterResult
                {
                    Success = false,
                    Message = "El nombre de usuario, correo electrónico o número de documento ya están registrados"
                };
            }

            var user = new ApplicationUser
            {
                ActivationToken = token,
                FirstName = model.FirstName,
                FirstLastName = model.FirstLastName,
                SecondLastName = model.SecondLastName,
                UserName = model.UserName,
                NormalizedUserName = model.UserName.ToUpper(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                DocumentType = model.DocumentType,
                DocumentNumber = model.DocumentNumber,
                BirthDate = model.BirthDate,
                Nationality = model.Nationality,
                Gender = model.Gender,
                Email = model.Email,
                SecondaryEmail = model.SecondaryEmail,
                PhoneNumber = model.PhoneNumber,
                SecondaryPhoneNumber = model.SecondaryPhoneNumber,
                TypeSecondaryPhoneNumber = model.TypeSecondaryPhoneNumber,
                TypeContract = model.TypeContract,
                HiringDate = model.HiringDate,
                ProfilePicture = model.ProfilePicture,
                IsActive = false,
                RoleId = (int)model.RoleId,
                MinistryId = (int)model.MinistryId
            };

            await _context.ApplicationUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            return new RegisterResult
            {
                Success = true,
                Message = "Usuario registrado correctamente"
            };
        }
    }
}
