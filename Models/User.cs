using System.ComponentModel.DataAnnotations;

namespace animal_adoption.Models
{
    public class User
    {
        [Key]
        [Required]
        public int id_user { get; set; }

        [Required]
        [MaxLength(50)]
        public string name { get; set; }

        [Required]
        [MaxLength(150)]
        public string password { get; set; }

        [Required]
        [MaxLength(15)]
        public string role { get; set; }

        [Required]
        [MaxLength(150)]
        public string email { get; set; }

        [Required]
        public bool status { get; set; }
        public string img { get; set; }
    }
}