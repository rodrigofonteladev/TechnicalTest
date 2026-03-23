using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string NormalizedName { get; set; } = null!;

        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
