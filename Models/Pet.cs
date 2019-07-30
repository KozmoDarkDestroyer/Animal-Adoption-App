using System.ComponentModel.DataAnnotations;

namespace animal_adoption.Models
{
    public class Pet
    {
        [Key]
        [Required]
        public int id_pet { get; set; }

        [Required]
        [MaxLength(6,ErrorMessage = "Maximum 6 characters allowed")]
        public string species { get; set; }

        [Required]
        [MaxLength(45,ErrorMessage = "Maximum 45 characters allowed")]
        public string race { get; set; }

        [Required]
        public int age { get; set; }

        [Required]
        [MaxLength(45,ErrorMessage = "Maximum 45 characters allowed")]
        public string name { get; set; }

        [Required]
        [MaxLength(15,ErrorMessage = "Maximum 15 characters allowed")]
        public string sex { get; set; }

        [MaxLength(500,ErrorMessage = "Maximum 500 characters allowed")]
        public string img { get; set; }

        [Required]
        public int id_foundation { get; set; }

        public Adopter Adopter { get; set; }

        public Foundation Foundation { get; set; }
    }
}