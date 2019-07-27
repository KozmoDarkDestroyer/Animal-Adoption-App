using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace animal_adoption.Models
{
    public class  Foundation
    {
        [Key]
        [Required]
        public int id_foundation { get; set; }

        [Required]
        [MaxLength(75,ErrorMessage = "Maximum 75 characters allowed")]
        public string name { get; set; }

        [Required]
        [MaxLength(150,ErrorMessage = "Maximum 145 characters allowed")]
        public string address { get; set; }

        [Required]
        [MaxLength(145,ErrorMessage = "Maximum 145 characters allowed")]
        public string association { get; set; }

        [Required]
        [MaxLength(150,ErrorMessage = "Maximum 150 characters allowed")]
        public string email { get; set; }

        [Required]
        [MaxLength(145,ErrorMessage = "Maximum 145 characters allowed")]
        public string web { get; set; }

        public List<Pet> Pets { get; set; }
    }
}