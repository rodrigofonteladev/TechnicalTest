using TechnicalTest.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Porfavor ingresa tu nombre")]
        [StringLength(100, ErrorMessage = "El nombre es demasiado largo")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Porfavor ingresa tu primer apellido")]
        [StringLength(100, ErrorMessage = "Tu primer apellido es demasiado largo")]
        public string FirstLastName { get; set; } = null!;

        [Required(ErrorMessage = "Porfavor ingresa tu segundo apellido")]
        [StringLength(100, ErrorMessage = "Tu segundo apellido es demasiado largo")]
        public string SecondLastName { get; set; } = null!;

        [Required(ErrorMessage = "Porfavor ingresa tu nombre de usuario")]
        [StringLength(50, ErrorMessage = "Tu nombre de usuario es demasiado largo")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Porfavor ingresa tu contraseña")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Tu contraseña debe tener entre {2} y {1} caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Porfavor repite tu contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; } = null!;

        [Required(ErrorMessage = "Porfavor selecciona tu tipo de documento")]
        public DocumentType? DocumentType { get; set; }

        [Required(ErrorMessage = "Porfavor ingresa el numero de tu documento")]
        [StringLength(20, ErrorMessage = "Tu numero de documento es demasiado largo")]
        public string DocumentNumber { get; set; } = null!;

        [Required(ErrorMessage = "Porfavor ingresa tu fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Porfavor selecciona tu nacionalidad")]
        public Nationality? Nationality { get; set; }

        [Required(ErrorMessage = "Porfavor selecciona tu genero")]
        public Gender? Gender { get; set; }

        [Required(ErrorMessage = "Porfavor ingresa tu direccion de correo electronico")]
        [StringLength(150, ErrorMessage = "Tu direccion de correo electronico es demasiado largo")]
        [EmailAddress(ErrorMessage = "Direccion de correo electronico invalido")]
        public string Email { get; set; } = null!;

        [StringLength(150, ErrorMessage = "Tu direccion de correo electronico segundario es demasiado largo")]
        [EmailAddress(ErrorMessage = "Direccion de correo electronico secundario invalido")]
        public string? SecondaryEmail { get; set; }

        [Required(ErrorMessage = "Porfavor ingresa tu numero de telefono")]
        [Phone(ErrorMessage = "Numero de telefono invalido")]
        public string PhoneNumber { get; set; } = null!;

        [Phone(ErrorMessage = "Numero de telefono secundario invalido")]
        public string? SecondaryPhoneNumber { get; set; }

        public PhoneType? TypeSecondaryPhoneNumber { get; set; }

        [Required(ErrorMessage = "Porfavor selecciona su tipo de contrato")]
        public TypeContract? TypeContract { get; set; }

        [Required(ErrorMessage = "Porfavor ingresa la fecha de contratacion")]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }

        public string? ProfilePicture { get; set; }

        [Required(ErrorMessage = "Porfavor selecciona tu rol")]
        public int? RoleId { get; set; }

        [Required(ErrorMessage = "Porfavor selecciona tu ministerio")]
        public int? MinistryId { get; set; }
    }
}
