using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Models
{
    public class Person
    {
        
        public int Id { get; set; }
        [Required, Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last name")]
        public string LastName { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        [Range(18,110)]
        public int Age { get; set; }
        public float Weight { get; set; }
        public int Height { get; set; }
        public string Sex { get; set; }
    }
}