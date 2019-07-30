using System.ComponentModel.DataAnnotations;

namespace animal_adoption.Models
{
    public class Form
    {
        [Key]
        [Required]
        public int id_form { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage = "Maximum 50 characters allowed")]
        public string name { get; set; }

        [Required]
        public int number_adults { get; set; }

        [Required]
        public int number_children { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage = "Maximum 50 characters allowed")]
        public int age_children { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage = "Maximum 50 characters allowed")]
        public string pet_race { get; set; }

        [Required]
        [MaxLength(75,ErrorMessage = "Maximum 75 characters allowed")]
        public string pets_before { get; set; }

        [Required]
        [MaxLength(150,ErrorMessage = "Maximum 150 characters allowed")]
        public string rason_adoption { get; set; }

        [Required]
        [MaxLength(150,ErrorMessage = "Maximum 150 characters allowed")]
        public string responsibility_pet { get; set; }

        [Required]
        public bool pet_status_check { get; set; }

        public string report { get; set; }

        [Required]
        public int id_adopter { get; set; }

        public Adopter Adopter { get; set; }
    }
}