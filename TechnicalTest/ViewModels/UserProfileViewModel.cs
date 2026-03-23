using TechnicalTest.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.ViewModels
{
    public class UserProfileViewModel
    {
        public string FullName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string FirstLastName { get; set; } = null!;
        public string SecondLastName { get; set; } = null!;
        public DocumentType? DocumentType { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public Nationality? Nationality { get; set; }
        public Gender? Gender { get; set; }
        public string Email { get; set; } = null!;
        public string? SecondaryEmail { get; set; }

        [DisplayFormat(DataFormatString = "+51 {0:### ### ###}", ApplyFormatInEditMode = false)]
        public string PhoneNumber { get; set; } = null!;
        public string? FormattedPhoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:### ### ###}", ApplyFormatInEditMode = true)]
        public string? SecondaryPhoneNumber { get; set; }
        public string? FormattedSecondaryPhoneNumber { get; set; }
        public PhoneType? TypeSecondaryPhoneNumber { get; set; }
        public TypeContract? TypeContract { get; set; }
        public DateTime HiringDate { get; set; }
        public string? ProfilePicture { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; } = null!;
        public string MinistryName { get; set; } = null!;
    }
}
