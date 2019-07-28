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
        [MaxLength(45)]
        public string name { get; set; }

        [Required]
        [MaxLength(50)]
        public string identification { get; set; }

        [Required]
        [MaxLength(45)]
        public string phone { get; set; }

        [Required]
        [MaxLength(150)]
        public string email { get; set; }

        [Required]
        [MaxLength(145)]
        public string address { get; set; }

        [Required]
        public int id_pet { get; set; }

        public List<Pet> Pets { get; set; }

        public List<Form> Forms { get; set; }
    }
}