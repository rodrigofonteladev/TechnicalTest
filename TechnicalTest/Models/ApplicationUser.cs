using TechnicalTest.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTest.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string? ActivationToken { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = null!;
        
        [Required]
        [StringLength(100)]
        public string FirstLastName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string SecondLastName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string NormalizedUserName { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [NotMapped]
        public string FullName => $"{FirstLastName} {SecondLastName}, {FirstName}";

        [Required]
        public DocumentType? DocumentType { get; set; }

        [Required]
        [StringLength(20)]
        public string DocumentNumber { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public Nationality? Nationality { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [EmailAddress]
        [StringLength(150)]
        public string? SecondaryEmail { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [NotMapped]
        public string FormattedPhoneNumber => $"+51 {long.Parse(PhoneNumber):### ### ###}";

        [Phone]
        public string? SecondaryPhoneNumber { get; set; }

        [NotMapped]
        public string SecondaryFormattedPhoneNumber
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SecondaryPhoneNumber)) return string.Empty;

                var pureNumbers = new string(PhoneNumber.Where(char.IsDigit).ToArray());

                if (long.TryParse(pureNumbers, out long number))
                {
                    return string.Format("+51 {0:### ### ###}", number);
                }

                return SecondaryPhoneNumber;
            }
        }

        public PhoneType? TypeSecondaryPhoneNumber { get; set; }

        [Required]
        public TypeContract? TypeContract { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockoutEnd { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public int MinistryId { get; set; }
        public Ministry Ministry { get; set; } = null!;
    }
}
