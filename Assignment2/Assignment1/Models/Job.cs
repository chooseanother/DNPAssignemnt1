using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Job
    {
        [Required, Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Range(0,1000000)]
        public int Salary { get; set; }
    }
}