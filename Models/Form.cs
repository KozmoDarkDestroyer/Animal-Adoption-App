using System.ComponentModel.DataAnnotations;

namespace animal_adoption.Models
{
    public class Form
    {
        [Key]
        [Required]
        public int id_form { get; set; }

        [Required]
        [MaxLength(500)]
        public string adotion_form { get; set; }

        [Required]
        public int id_adopter { get; set; }

        public Adopter Adopter { get; set; }
    }
}