using System.ComponentModel.DataAnnotations;

namespace FamilyWebAPI.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        public string HairColor { get; set; }
        [Required]
        public string EyeColor { get; set; }
        [Range(0,110)]
        public int Age { get; set; }
        public float Weight { get; set; }
        [Range(50,300)]
        public int Height { get; set; }
        [Required(ErrorMessage = "Chose a Sex")]
        public string Sex { get; set; }
    }
}