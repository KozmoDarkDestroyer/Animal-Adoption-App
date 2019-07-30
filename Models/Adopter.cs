using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace animal_adoption.Models
{
    public class Adopter
    {
        [Key]
        [Required]
        public int id_adopter { get; set; }

        [Required]
        [MaxLength(45,ErrorMessage = "Maximum 45 characters allowed")]
        public string name { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage = "Maximum 50 characters allowed")]
        public string identification { get; set; }

        [Required]
        [MaxLength(45,ErrorMessage = "Maximum 45 characters allowed")]
        public string phone { get; set; }

        [Required]
        [MaxLength(150,ErrorMessage = "Maximum 150 characters allowed")]
        public string email { get; set; }

        [Required]
        [MaxLength(145,ErrorMessage = "Maximum 145 characters allowed")]
        public string address { get; set; }

        [Required]
        public int id_pet { get; set; }

        public Pet Pet { get; set; }
        
        public Form Form { get; set; }
    }
}