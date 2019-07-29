using System.ComponentModel.DataAnnotations;

namespace animal_adoption.Models
{
    public class Form
    {
        [Key]
        [Required]
        public int id_form { get; set; }

        [Required]
        [MaxLength(50)]
        public string name { get; set; }

        [Required]
        [MaxLength(500)]
        public string report { get; set; }

        [Required]
        public int id_adopter { get; set; }

        public Adopter Adopter { get; set; }
    }
}