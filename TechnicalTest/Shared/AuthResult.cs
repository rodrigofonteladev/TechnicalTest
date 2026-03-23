using TechnicalTest.Models;

namespace TechnicalTest.Shared
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public bool IsLocked { get; set; }
        public string? Message { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
