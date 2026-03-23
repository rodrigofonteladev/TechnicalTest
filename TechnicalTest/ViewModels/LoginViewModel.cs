using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Por favor, ingresa tu nombre de usuario")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Por favor, ingresa tu contraseña")]
        public string Password { get; set; } = null!;
    }
}
