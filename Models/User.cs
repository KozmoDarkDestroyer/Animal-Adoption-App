using System.ComponentModel.DataAnnotations;

namespace animal_adoption.Models
{
    public class User
    {
        [Key]
        [Required]
        public int id_user { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage = "Maximum 50 characters allowed")]
        public string name { get; set; }

        [Required]
        [MaxLength(150,ErrorMessage = "Maximum 150 characters allowed")]
        public string password { get; set; }

        [Required]
        [MaxLength(15,ErrorMessage = "Maximum 15 characters allowed")]
        public string role { get; set; }

        [Required]
        [MaxLength(150,ErrorMessage = "Maximum 150 characters allowed")]
        public string email { get; set; }

        [Required]
        public bool status { get; set; }
        public string img { get; set; }
    }
}